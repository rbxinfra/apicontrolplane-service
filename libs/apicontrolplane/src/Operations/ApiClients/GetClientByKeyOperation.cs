namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting a client.
/// </summary>
public class GetClientByKeyOperation : IResultOperation<GetClientByKeyRequest, ApiClientModel>
{
    private readonly ILogger _Logger;
    private readonly IApiClientFactory _ApiClientFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetClientByKeyOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// </exception>
    public GetClientByKeyOperation(ILogger logger, IApiClientFactory apiClientFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ApiClientModel, OperationError) Execute(GetClientByKeyRequest input)
    {
        _Logger.Information("GetClientByKey, Key = {0}", input.Key);

        var apiClient = _ApiClientFactory.GetByKey(input.Key);

        return (apiClient != null ? new(apiClient) : null, null);
    }
}
