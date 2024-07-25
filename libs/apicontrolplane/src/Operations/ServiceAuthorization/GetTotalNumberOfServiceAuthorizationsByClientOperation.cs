namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting total number of service authorizations by client.
/// </summary>
public class GetTotalNumberOfServiceAuthorizationsByClientOperation
    : IResultOperation<GetTotalNumberOfServiceAuthorizationsByClientRequest, int>
{
    private readonly ILogger _Logger;
    private readonly IApiClientFactory _ApiClientFactory;
    private readonly IServiceAuthorizationFactory _ServiceAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetTotalNumberOfServiceAuthorizationsByClientOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <param name="serviceAuthorizationFactory">The <see cref="IServiceAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// - <paramref name="serviceAuthorizationFactory"/> cannot be null.
    /// </exception>
    public GetTotalNumberOfServiceAuthorizationsByClientOperation(
        ILogger logger, 
        IApiClientFactory apiClientFactory, 
        IServiceAuthorizationFactory serviceAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
        _ServiceAuthorizationFactory = serviceAuthorizationFactory ?? throw new ArgumentNullException(nameof(serviceAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation.Execute"/>
    public (int, OperationError) Execute(GetTotalNumberOfServiceAuthorizationsByClientRequest input)
    {
        _Logger.Information("GetTotalNumberOfServiceAuthorizationsByClient, Key = {0}", input.Key);

        var apiClient = _ApiClientFactory.GetByKey(input.Key);
        if (apiClient == null) return (default, new(ApiControlPlaneErrors.UnknownClient, input.Key));

        return (_ServiceAuthorizationFactory.GetTotalNumberByApiClient(apiClient), null);
    }
}
