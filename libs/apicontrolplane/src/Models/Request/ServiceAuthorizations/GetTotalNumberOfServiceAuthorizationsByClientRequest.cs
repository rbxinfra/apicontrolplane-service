namespace Roblox.ApiControlPlane.Models;

using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get total number of service authorizations by client operation.
/// </summary>
public class GetTotalNumberOfServiceAuthorizationsByClientRequest
{
    /// <summary>
    /// Gets or sets the key of the api client.
    /// </summary>
    [FromQuery(Name = "key")]
    [Required]
    public Guid Key { get; set; }
}
