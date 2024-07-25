namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

/// <summary>
/// Operation for getting total number of clients.
/// </summary>
public class GetTotalNumberOfClientsOperation : IResultOperation<int>
{
    private readonly ILogger _Logger;
    private readonly IApiClientFactory _ApiClientFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetTotalNumberOfClientsOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// </exception>
    public GetTotalNumberOfClientsOperation(ILogger logger, IApiClientFactory apiClientFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
    }

    /// <inheritdoc cref="IOperation.Execute"/>
    public (int, OperationError) Execute()
    {
        _Logger.Information("GetTotalNumberOfClients");

        return (_ApiClientFactory.GetTotalNumber(), null);
    }
}
