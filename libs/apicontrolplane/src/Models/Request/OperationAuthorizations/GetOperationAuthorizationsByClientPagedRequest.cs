namespace Roblox.ApiControlPlane.Models;

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get operation authorizations by client paged operation.
/// </summary>
public class GetOperationAuthorizationsByClientPagedRequest
{
    /// <summary>
    /// Gets or sets the key of the api client.
    /// </summary>
    [FromQuery(Name = "key")]
    [Required]
    public Guid Key { get; set; }

    /// <summary>
    /// Gets or sets start row index.
    /// </summary>
    [FromQuery(Name = "startRowIndex")]
    [Required]
    [DefaultValue(1)]
    public int StartRowIndex { get; set; } = 1;

    /// <summary>
    /// Gets or sets the maximum rows.
    /// </summary>
    [FromQuery(Name = "maximumRows")]
    [Required]
    [DefaultValue(50)]
    public int MaximumRows { get; set; } = 50;
}
