namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for creating a new client.
/// </summary>
public class AddClientOperation : IResultOperation<AddClientPostData, ApiClientModel>
{
    private readonly ILogger _Logger;
    private readonly IApiClientFactory _ApiClientFactory;

    /// <summary>
    /// Construct a new instance of <see cref="AddClientOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// </exception>
    public AddClientOperation(ILogger logger, IApiClientFactory apiClientFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ApiClientModel, OperationError) Execute(AddClientPostData input)
    {
        if (string.IsNullOrEmpty(input.Note)) return (null, new("Note cannot be null or empty!"));

        var apiKey = input.Key ?? Guid.NewGuid();

        _Logger.Information("AddApiClient, ApiKey = {0}, Note = {1}, IsValid = {2}", apiKey, input.Note, input.IsValid);

        var apiClient = _ApiClientFactory.GetByKey(apiKey);
        if (apiClient != null) return (null, new(ApiControlPlaneErrors.ClientAlreadyExists, apiKey, apiClient.Note));

        return (new(_ApiClientFactory.CreateNew(apiKey, input.Note, input.IsValid)), null);
    }
}
