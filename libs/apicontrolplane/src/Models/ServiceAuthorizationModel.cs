namespace Roblox.ApiControlPlane.Models;

using System;

using Service.ApiControlPlane;

/// <summary>
/// The model for a <see cref="IServiceAuthorization"/>
/// </summary>
public class ServiceAuthorizationModel
{
    /// <summary>
    /// Construct a new instance of <see cref="OperationModel"/>
    /// </summary>
    /// <param name="serviceAuthorization">The <see cref="IServiceAuthorization"/></param>
    /// <exception cref="ArgumentNullException"><paramref name="serviceAuthorization"/> cannot be null.</exception>
    public ServiceAuthorizationModel(IServiceAuthorization serviceAuthorization)
    {
        if (serviceAuthorization == null) throw new ArgumentNullException(nameof(serviceAuthorization));

        Id = serviceAuthorization.ID;
        ServiceId = serviceAuthorization.Service.ID;
        ApiClientId = serviceAuthorization.ApiClient.ID;
        AuthorizationType = serviceAuthorization.AuthorizationType;
        Created = serviceAuthorization.Created;
        Updated = serviceAuthorization.Updated;
    }

    /// <summary>
    /// Gets or sets the ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the service.
    /// </summary>
    public int ServiceId { get; set; }


    /// <summary>
    /// Gets or sets the ID of the api client.
    /// </summary>
    public int ApiClientId { get; set; }

    /// <summary>
    /// Gets or sets the authorization type.
    /// </summary>
    public AuthorizationTypeEnum AuthorizationType { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    public DateTime Updated { get; set; }
}
