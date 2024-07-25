namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting a client.
/// </summary>
public class GetClientByIDOperation : IResultOperation<GetClientByIDRequest, ApiClientModel>
{
    private readonly ILogger _Logger;
    private readonly IApiClientFactory _ApiClientFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetClientByIDOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// </exception>
    public GetClientByIDOperation(ILogger logger, IApiClientFactory apiClientFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ApiClientModel, OperationError) Execute(GetClientByIDRequest input)
    {
        _Logger.Information("GetClientByID, ID = {0}", input.Id);

        var apiClient = _ApiClientFactory.GetByID(input.Id);

        return (apiClient != null ? new(apiClient) : null, null);
    }
}
