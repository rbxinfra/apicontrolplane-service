namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post data for the remove client operation.
/// </summary>
public class RemoveClientPostData
{
    /// <summary>
    /// Gets or sets the ID of the client.
    /// </summary>
    [Required]
    public int Id { get; set; }
}
