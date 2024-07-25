namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting a service registration.
/// </summary>
public class GetServiceRegistrationOperation : IResultOperation<GetServiceRegistrationRequest, ServiceRegistrationModel>
{
    private readonly ILogger _Logger;
    private readonly IServiceRegistrationFactory _ServiceRegistrationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetServiceRegistrationOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceRegistrationFactory">The <see cref="IServiceRegistrationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceRegistrationFactory"/> cannot be null.
    /// </exception>
    public GetServiceRegistrationOperation(ILogger logger, IServiceRegistrationFactory serviceRegistrationFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceRegistrationFactory = serviceRegistrationFactory ?? throw new ArgumentNullException(nameof(serviceRegistrationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ServiceRegistrationModel, OperationError) Execute(GetServiceRegistrationRequest input)
    {
        if (string.IsNullOrEmpty(input.ServiceName)) return (null, new("{0} cannot be null or empty", nameof(input.ServiceName)));

        _Logger.Information("GetServiceRegistration, ServiceName = {0}", input.ServiceName);

        var serviceRegistration = _ServiceRegistrationFactory.GetServiceRegistration(input.ServiceName);

        return (serviceRegistration != null ? new(serviceRegistration) : null, null);
    }
}
