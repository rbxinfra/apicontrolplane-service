namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get client by id operation.
/// </summary>
public class GetClientByIDRequest
{
    /// <summary>
    /// Gets or sets the ID of the client
    /// </summary>
    [FromQuery(Name = "id")]
    [Required]
    public int Id { get; set; }
}
