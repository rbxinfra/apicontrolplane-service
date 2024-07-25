namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post data for the remove service operation.
/// </summary>
public class RemoveServicePostData
{
    /// <summary>
    /// Gets or sets the ID of the service.
    /// </summary>
    [Required]
    public int Id { get; set; }
}
