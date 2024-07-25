namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get service registration operation.
/// </summary>
public class GetServiceRegistrationRequest
{
    /// <summary>
    /// Gets or sets the name of the service.
    /// </summary>
    [FromQuery(Name = "serviceName")]
    [Required]
    public string ServiceName { get; set; }
}
