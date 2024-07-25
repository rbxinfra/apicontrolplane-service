namespace Roblox.ApiControlPlane;

using System;
using System.Threading.Tasks;

using EventLog;
using Operations;
using Api.ControlPlane;
using Service.ApiControlPlane;

using Models;

/// <summary>
/// Operation for removing a client.
/// </summary>
public class RemoveClientOperation : IOperation<RemoveClientPostData>
{
    private readonly ILogger _Logger;
    private readonly IApiClientFactory _ApiClientFactory;
    private readonly IServiceAuthorizationFactory _ServiceAuthorizationFactory;
    private readonly IOperationAuthorizationFactory _OperationAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="RemoveClientOperation"/>
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
    public RemoveClientOperation(
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
    public OperationError Execute(RemoveClientPostData input)
    {
        _Logger.Information("RemoveClient, ID = {1}", input.Id);

        var apiClient = _ApiClientFactory.GetByID(input.Id);
        if (apiClient == null) return new(ApiControlPlaneErrors.UnknownClient, input.Id);

        _Logger.Warning("RemoveClient: Setting client {0} to invalid before removing it!", apiClient.ApiKey);

        apiClient.SetInvalid();

        Task.Run(() => DoRemoveClient(apiClient));

        return null;
    }

    private void DoRemoveClient(IApiClient apiClient)
    {
        _Logger.Information("RemoveClient: Deleting authorizations for ApiClient '{0}'", apiClient.ApiKey);

        var apiClientServiceAuthorizations = _ServiceAuthorizationFactory.GetAllByApiClient(apiClient);
        var apiClientOperationAuthorizations = _OperationAuthorizationFactory.GetAllByApiClient(apiClient);

        foreach (var serviceAuthorization in apiClientServiceAuthorizations)
            serviceAuthorization.Delete();

        foreach (var operationAuthorization in apiClientOperationAuthorizations)
            operationAuthorization.Delete();

        apiClient.Delete();
    }
}
