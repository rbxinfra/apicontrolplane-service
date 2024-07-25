namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting a service.
/// </summary>
public class GetServiceByNameOperation : IResultOperation<GetServiceByNameRequest, ServiceModel>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetServiceByNameOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// </exception>
    public GetServiceByNameOperation(ILogger logger, IServiceFactory serviceFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ServiceModel, OperationError) Execute(GetServiceByNameRequest input)
    {
        if (string.IsNullOrEmpty(input.Name)) return (null, new("{0} cannot be null or empty", nameof(input.Name)));

        _Logger.Information("GetServiceByName, Name = {0}", input.Name);

        var service = _ServiceFactory.GetByName(input.Name);

        return (service != null ? new(service) : null, null);
    }
}
