namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get total number of service authorizations by service operation.
/// </summary>
public class GetTotalNumberOfServiceAuthorizationsByServiceRequest
{
    /// <summary>
    /// Gets or sets the name of the service
    /// </summary>
    [FromQuery(Name = "serviceName")]
    [Required]
    public string ServiceName { get; set; }
}
