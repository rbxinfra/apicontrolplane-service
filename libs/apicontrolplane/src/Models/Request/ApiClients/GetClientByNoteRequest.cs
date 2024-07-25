namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get client by note operation.
/// </summary>
public class GetClientByNoteRequest
{
    /// <summary>
    /// Gets or sets the note of the client
    /// </summary>
    [FromQuery(Name = "note")]
    [Required]
    public string Note { get; set; }
}
