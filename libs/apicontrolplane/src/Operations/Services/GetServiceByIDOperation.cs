namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting a service.
/// </summary>
public class GetServiceByIDOperation : IResultOperation<GetServiceByIDRequest, ServiceModel>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetServiceByIDOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// </exception>
    public GetServiceByIDOperation(ILogger logger, IServiceFactory serviceFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ServiceModel, OperationError) Execute(GetServiceByIDRequest input)
    {
        _Logger.Information("GetServiceByID, ID = {0}", input.Id);

        var service = _ServiceFactory.GetByID(input.Id);

        return (service != null ? new(service) : null, null);
    }
}
