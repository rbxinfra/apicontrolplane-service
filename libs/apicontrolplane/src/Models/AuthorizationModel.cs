namespace Roblox.ApiControlPlane.Models;

using System;

using Service.ApiControlPlane;

/// <summary>
/// The model for a <see cref="IAuthorization"/>
/// </summary>
public class AuthorizationModel
{
    /// <summary>
    /// Construct a new instance of <see cref="OperationModel"/>
    /// </summary>
    /// <param name="authorization">The <see cref="IAuthorization"/></param>
    /// <exception cref="ArgumentNullException"><paramref name="authorization"/> cannot be null.</exception>
    public AuthorizationModel(IAuthorization authorization)
    {
        if (authorization == null) throw new ArgumentNullException(nameof(authorization));

        ApiClientId = authorization.ApiClient.ID;
        ApiClientNote = authorization.ApiClient.Note;
        ServiceName = authorization.ServiceName;
        OperationName = authorization.Operation?.Name;
        AuthorizationType = authorization.AuthorizationType;
    }

    /// <summary>
    /// Gets or sets the ID of the api client.
    /// </summary>
    public int ApiClientId { get; set; }

    /// <summary>
    /// Gets or sets the Note of the api client.
    /// </summary>
    public string ApiClientNote { get; set; }

    /// <summary>
    /// Gets or sets the service name
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// Gets or sets the operation name
    /// </summary>
    public string OperationName { get; set; }

    /// <summary>
    /// Gets or sets the authorization type.
    /// </summary>
    public AuthorizationTypeEnum AuthorizationType { get; set; }
}
