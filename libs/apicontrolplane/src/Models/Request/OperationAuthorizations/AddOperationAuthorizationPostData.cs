namespace Roblox.ApiControlPlane.Models;

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post data for the add operation authorization operation.
/// </summary>
public class AddOperationAuthorizationPostData
{
    /// <summary>
    /// Gets or sets the name of the service
    /// </summary>
    [Required]
    public string ServiceName { get; set; }

    /// <summary>
    /// Gets or sets the name of the operation
    /// </summary>
    [Required]
    public string OperationName { get; set; }

    /// <summary>
    /// Gets or sets the key of the API client.
    /// </summary>
    [Required]
    public Guid Key { get; set; }

    /// <summary>
    /// Gets or sets the authorization type.
    /// </summary>
    [Required]
    [DefaultValue(AuthorizationType.Full)]
    public AuthorizationType AuthorizationType { get; set; } = AuthorizationType.Full;
}
