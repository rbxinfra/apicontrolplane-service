namespace Roblox.ApiControlPlane.Models;

using System;
using System.Linq;
using System.Collections.Generic;

using Service.ApiControlPlane;

/// <summary>
/// The model for a <see cref="IService"/>
/// </summary>
public class ServiceRegistrationModel
{
    /// <summary>
    /// Construct a new instance of <see cref="ServiceRegistrationModel"/>
    /// </summary>
    /// <param name="serviceRegistration">The <see cref="IServiceRegistration"/></param>
    /// <exception cref="ArgumentNullException"><paramref name="serviceRegistration"/> cannot be null.</exception>
    public ServiceRegistrationModel(IServiceRegistration serviceRegistration)
    {
        if (serviceRegistration == null) throw new ArgumentNullException(nameof(serviceRegistration));

        ServiceName = serviceRegistration.ServiceName;
        IsEnabled = serviceRegistration.IsEnabled;
        ApiClients = serviceRegistration.ApiClients.Select(client => new ApiClientModel(client)).ToArray();
        Operations = serviceRegistration.Operations.Select(operation => new OperationModel(operation)).ToArray();
        Authorizations = serviceRegistration.Authorizations.Select(authorization => new AuthorizationModel(authorization)).ToArray();
    }

    /// <summary>
    /// Gets the service name.
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// Determines if the service is enabled.
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Gets or sets the Api Clients.
    /// </summary>
    public ICollection<ApiClientModel> ApiClients { get; set; }

    /// <summary>
    /// Gets or sets the operations.
    /// </summary>
    public ICollection<OperationModel> Operations { get; set; }

    /// <summary>
    /// Gets or sets the authorizations.
    /// </summary>
    public ICollection<AuthorizationModel> Authorizations { get; set; }
}
