namespace Roblox.ApiControlPlane.Models;

using System;

using Service.ApiControlPlane;

/// <summary>
/// The model for a <see cref="IOperationAuthorization"/>
/// </summary>
public class OperationAuthorizationModel
{
    /// <summary>
    /// Construct a new instance of <see cref="OperationModel"/>
    /// </summary>
    /// <param name="operationAuthorization">The <see cref="IOperationAuthorization"/></param>
    /// <exception cref="ArgumentNullException"><paramref name="operationAuthorization"/> cannot be null.</exception>
    public OperationAuthorizationModel(IOperationAuthorization operationAuthorization)
    {
        if (operationAuthorization == null) throw new ArgumentNullException(nameof(operationAuthorization));

        Id = operationAuthorization.ID;
        OperationId = operationAuthorization.Operation.ID;
        ApiClientId = operationAuthorization.ApiClient.ID;
        AuthorizationType = operationAuthorization.AuthorizationType;
        Created = operationAuthorization.Created;
        Updated = operationAuthorization.Updated;
    }

    /// <summary>
    /// Gets or sets the ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the ID of the service.
    /// </summary>
    public int OperationId { get; set; }


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
