namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get operation by id operation.
/// </summary>
public class GetOperationByIDRequest
{
    /// <summary>
    /// Gets or sets the ID of the operation
    /// </summary>
    [FromQuery(Name = "id")]
    [Required]
    public int Id { get; set; }
}
