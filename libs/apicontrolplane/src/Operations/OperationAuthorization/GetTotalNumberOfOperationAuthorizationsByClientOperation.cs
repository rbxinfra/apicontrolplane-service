namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting total number of operation authorizations by client.
/// </summary>
public class GetTotalNumberOfOperationAuthorizationsByClientOperation
    : IResultOperation<GetTotalNumberOfOperationAuthorizationsByClientRequest, int>
{
    private readonly ILogger _Logger;
    private readonly IApiClientFactory _ApiClientFactory;
    private readonly IOperationAuthorizationFactory _OperationAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetTotalNumberOfOperationAuthorizationsByClientOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <param name="operationAuthorizationFactory">The <see cref="IOperationAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// - <paramref name="operationAuthorizationFactory"/> cannot be null.
    /// </exception>
    public GetTotalNumberOfOperationAuthorizationsByClientOperation(
        ILogger logger, 
        IApiClientFactory apiClientFactory, 
        IOperationAuthorizationFactory operationAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
        _OperationAuthorizationFactory = operationAuthorizationFactory ?? throw new ArgumentNullException(nameof(operationAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation.Execute"/>
    public (int, OperationError) Execute(GetTotalNumberOfOperationAuthorizationsByClientRequest input)
    {
        _Logger.Information("GetTotalNumberOfOperationAuthorizationsByClient, Key = {0}", input.Key);

        var apiClient = _ApiClientFactory.GetByKey(input.Key);
        if (apiClient == null) return (default, new(ApiControlPlaneErrors.UnknownClient, input.Key));

        return (_OperationAuthorizationFactory.GetTotalNumberByApiClient(apiClient), null);
    }
}
