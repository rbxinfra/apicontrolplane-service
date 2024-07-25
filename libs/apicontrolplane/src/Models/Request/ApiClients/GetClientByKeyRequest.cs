namespace Roblox.ApiControlPlane.Models;

using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get client by key operation.
/// </summary>
public class GetClientByKeyRequest
{
    /// <summary>
    /// Gets or sets the key of the client
    /// </summary>
    [FromQuery(Name = "key")]
    [Required]
    public Guid Key { get; set; }
}
