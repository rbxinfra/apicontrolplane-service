namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get service by id operation.
/// </summary>
public class GetServiceByIDRequest
{
    /// <summary>
    /// Gets or sets the ID of the client
    /// </summary>
    [FromQuery(Name = "id")]
    [Required]
    public int Id { get; set; }
}
