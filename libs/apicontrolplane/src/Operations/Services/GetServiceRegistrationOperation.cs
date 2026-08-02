namespace Roblox.ApiControlPlane;

using System;
using System.Linq;

using Microsoft.AspNetCore.Http;

using EventLog;
using Operations;
using Api.ControlPlane;

using Web.Framework.Services.Http;

using Models;

/// <summary>
/// Operation for getting a service registration.
/// </summary>
public class GetServiceRegistrationOperation : IResultOperation<GetServiceRegistrationRequest, ServiceRegistrationModel>
{
    private readonly ILogger _Logger;
    private readonly IServiceRegistrationFactory _ServiceRegistrationFactory;
    private readonly IAuthorizationFactory _AuthorizationFactory;
    private readonly IHttpContextAccessor _HttpContextAccessor;

    /// <summary>
    /// Construct a new instance of <see cref="GetServiceRegistrationOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceRegistrationFactory">The <see cref="IServiceRegistrationFactory"/></param>
    /// <param name="authorizationFactory">The <see cref="IAuthorizationFactory"/></param>
    /// <param name="httpContextAccessor">The <see cref="IHttpContextAccessor"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceRegistrationFactory"/> cannot be null.
    /// - <paramref name="authorizationFactory"/> cannot be null.
    /// - <paramref name="httpContextAccessor"/> cannot be null.
    /// </exception>
    public GetServiceRegistrationOperation(
        ILogger logger, 
        IServiceRegistrationFactory serviceRegistrationFactory,
        IAuthorizationFactory authorizationFactory,
        IHttpContextAccessor httpContextAccessor
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceRegistrationFactory = serviceRegistrationFactory ?? throw new ArgumentNullException(nameof(serviceRegistrationFactory));
        _AuthorizationFactory = authorizationFactory ?? throw new ArgumentNullException(nameof(authorizationFactory));
        _HttpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ServiceRegistrationModel, OperationError) Execute(GetServiceRegistrationRequest input)
    {
        if (string.IsNullOrEmpty(input.ServiceName)) return (null, new("{0} cannot be null or empty", nameof(input.ServiceName)));

        var currentClient = _HttpContextAccessor.HttpContext?.GetCurrentApiClient();

        _Logger.Information("GetServiceRegistration, ServiceName = {0}", input.ServiceName);

        var serviceRegistration = _ServiceRegistrationFactory.GetServiceRegistration(input.ServiceName);

        if (serviceRegistration is not null && currentClient is not null && !serviceRegistration.ApiClients.Any(c => c.ID == currentClient?.ID))
        {
            _Logger.Information("GetServiceRegistration, ServiceName = {0}, Client '{1}' is not authorized to access this service registration", input.ServiceName, currentClient?.Note);  

            return (null, new(ApiControlPlaneErrors.UnauthorizedServiceRegistrationAccess, currentClient?.Note, input.ServiceName)); 
        }

        return (serviceRegistration != null ? new(serviceRegistration) : null, null);
    }
}
