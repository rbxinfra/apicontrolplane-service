namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

using Microsoft.AspNetCore.Mvc;

/// <summary>
/// Get request for the get total number of operation authorizations by operation operation.
/// </summary>
public class GetTotalNumberOfOperationAuthorizationsByOperationRequest
{
    /// <summary>
    /// Gets or sets the name of the service
    /// </summary>
    [FromQuery(Name = "serviceName")]
    [Required]
    public string ServiceName { get; set; }

    /// <summary>
    /// Gets or sets the name of the operation
    /// </summary>
    [FromQuery(Name = "operationName")]
    [Required]
    public string OperationName { get; set; }
}
