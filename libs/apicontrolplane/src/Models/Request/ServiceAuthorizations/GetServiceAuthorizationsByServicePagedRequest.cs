﻿namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get service authorizations by service paged operation.
/// </summary>
public class GetServiceAuthorizationsByServicePagedRequest
{
    /// <summary>
    /// Gets or sets the name of the service
    /// </summary>
    [FromQuery(Name = "serviceName")]
    [Required]
    public string ServiceName { get; set; }

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
