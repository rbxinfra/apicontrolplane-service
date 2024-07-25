namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for updating a client.
/// </summary>
public class UpdateClientOperation : IResultOperation<UpdateClientPostData, ApiClientModel>
{
    private readonly ILogger _Logger;
    private readonly IApiClientFactory _ApiClientFactory;

    /// <summary>
    /// Construct a new instance of <see cref="UpdateClientOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// </exception>
    public UpdateClientOperation(ILogger logger, IApiClientFactory apiClientFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ApiClientModel, OperationError) Execute(UpdateClientPostData input)
    {
        var apiClient = _ApiClientFactory.GetByID(input.Id);
        if (apiClient == null) return (null, new(ApiControlPlaneErrors.UnknownClient, input.Id));

        _Logger.Information("UpdateClient, ID = {0}", input.Id);

        if (!string.IsNullOrEmpty(input.Note))
        {
            apiClient.Note = input.Note;

            _Logger.Information("UpdateClient: New Note = {0}", input.Note);
        }

        if (input.Key is not null && input.Key.Value != apiClient.ApiKey)
        {
            var existingClient = _ApiClientFactory.GetByKey(input.Key.Value);
            if (existingClient != null) return (null, new(ApiControlPlaneErrors.ClientAlreadyExists, input.Key.Value, existingClient.Note));

            apiClient.ApiKey = input.Key.Value;

            _Logger.Information("UpdateClient: New Key = {0}", input.Key.Value);
        }

        if (input.IsValid)
            apiClient.SetValid();
        else
            apiClient.SetInvalid();

        return (new(apiClient), null);
    }
}
