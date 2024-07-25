namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get service by name operation.
/// </summary>
public class GetServiceByNameRequest
{
    /// <summary>
    /// Gets or sets the name of the service
    /// </summary>
    [FromQuery(Name = "name")]
    [Required]
    public string Name { get; set; }
}
