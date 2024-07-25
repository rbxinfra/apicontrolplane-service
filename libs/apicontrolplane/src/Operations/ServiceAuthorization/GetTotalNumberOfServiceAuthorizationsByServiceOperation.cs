namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting total number of service authorizations by service.
/// </summary>
public class GetTotalNumberOfServiceAuthorizationsByServiceOperation 
    : IResultOperation<GetTotalNumberOfServiceAuthorizationsByServiceRequest, int>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IServiceAuthorizationFactory _ServiceAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetTotalNumberOfServiceAuthorizationsByServiceOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="serviceAuthorizationFactory">The <see cref="IServiceAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="serviceAuthorizationFactory"/> cannot be null.
    /// </exception>
    public GetTotalNumberOfServiceAuthorizationsByServiceOperation(
        ILogger logger, 
        IServiceFactory serviceFactory, 
        IServiceAuthorizationFactory serviceAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _ServiceAuthorizationFactory = serviceAuthorizationFactory ?? throw new ArgumentNullException(nameof(serviceAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation.Execute"/>
    public (int, OperationError) Execute(GetTotalNumberOfServiceAuthorizationsByServiceRequest input)
    {
        if (string.IsNullOrEmpty(input.ServiceName)) return (default, new("{0} cannot be null or empty", nameof(input.ServiceName)));

        _Logger.Information("GetTotalNumberOfServiceAuthorizationsByService, ServiceName = {0}", input.ServiceName);

        var service = _ServiceFactory.GetByName(input.ServiceName);
        if (service == null) return (default, new(ApiControlPlaneErrors.UnknownService, input.ServiceName));

        return (_ServiceAuthorizationFactory.GetTotalNumberByService(service), null);
    }
}
