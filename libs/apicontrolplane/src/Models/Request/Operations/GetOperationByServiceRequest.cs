namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get operation by service operation.
/// </summary>
public class GetOperationByServiceRequest
{
    /// <summary>
    /// Gets or sets the name of the operation
    /// </summary>
    [FromQuery(Name = "name")]
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the name of the service
    /// </summary>
    [FromQuery(Name = "serviceName")]
    [Required]
    public string ServiceName { get; set; }
}
