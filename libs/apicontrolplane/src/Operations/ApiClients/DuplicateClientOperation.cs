namespace Roblox.ApiControlPlane;

using System;
using System.Threading.Tasks;

using EventLog;
using Operations;
using Api.ControlPlane;
using Service.ApiControlPlane;

using Models;

/// <summary>
/// Operation for creating a new client.
/// </summary>
public class DuplicateClientOperation : IResultOperation<DuplicateClientPostData, ApiClientModel>
{
    private readonly ILogger _Logger;
    private readonly IApiClientFactory _ApiClientFactory;
    private readonly IServiceAuthorizationFactory _ServiceAuthorizationFactory;
    private readonly IOperationAuthorizationFactory _OperationAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="DuplicateClientOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <param name="serviceAuthorizationFactory">The <see cref="IServiceAuthorizationFactory"/></param>
    /// <param name="operationAuthorizationFactory">The <see cref="IOperationAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// - <paramref name="serviceAuthorizationFactory"/> cannot be null.
    /// - <paramref name="operationAuthorizationFactory"/> cannot be null.
    /// </exception>
    public DuplicateClientOperation(
        ILogger logger,
        IApiClientFactory apiClientFactory,
        IServiceAuthorizationFactory serviceAuthorizationFactory,
        IOperationAuthorizationFactory operationAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
        _ServiceAuthorizationFactory = serviceAuthorizationFactory ?? throw new ArgumentNullException(nameof(serviceAuthorizationFactory));
        _OperationAuthorizationFactory = operationAuthorizationFactory ?? throw new ArgumentNullException(nameof(operationAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ApiClientModel, OperationError) Execute(DuplicateClientPostData input)
    {
        var apiKey = input.Key ?? Guid.NewGuid();

        _Logger.Information("DuplicateClient, ID = {0}, ApiKey = {1}", input, apiKey);

        var apiClient = _ApiClientFactory.GetByID(input.Id);
        if (apiClient == null) return (null, new(ApiControlPlaneErrors.UnknownClient, input.Id));

        var note = input.Note;
        if (string.IsNullOrEmpty(note)) note = $"{apiClient.Note} (Duplicate)";

        _Logger.Information("DuplicateClient: New Note = {0}", note);

        if (apiKey == apiClient.ApiKey) return (null, new(ApiControlPlaneErrors.ClientKeyConflict));

        var existingClient = _ApiClientFactory.GetByKey(apiKey);
        if (existingClient != null) return (null, new(ApiControlPlaneErrors.ClientAlreadyExists, apiKey, existingClient.Note));

        var newApiClient = _ApiClientFactory.CreateNew(apiKey, note);

        Task.Run(() => DuplicateClientAuthorizations(apiClient, newApiClient));

        return (new(newApiClient), null);
    }

    private void DuplicateClientAuthorizations(IApiClient original, IApiClient duplicated)
    {
        _Logger.Information("DuplicateClient: Copying authorizations for ApiClient '{0}' to '{1}'", original.ApiKey, duplicated.ApiKey);

        var apiClientServiceAuthorizations = _ServiceAuthorizationFactory.GetAllByApiClient(original);
        var apiClientOperationAuthorizations = _OperationAuthorizationFactory.GetAllByApiClient(original);

        foreach (var serviceAuthorization in apiClientServiceAuthorizations)
            _ServiceAuthorizationFactory.CreateNew(duplicated, serviceAuthorization.Service, serviceAuthorization.AuthorizationType);

        foreach (var operationAuthorization in apiClientOperationAuthorizations)
            _OperationAuthorizationFactory.CreateNew(duplicated, operationAuthorization.Operation, operationAuthorization.AuthorizationType);
    }
}
