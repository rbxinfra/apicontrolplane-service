namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post data for the remove operation operation.
/// </summary>
public class RemoveOperationPostData
{
    /// <summary>
    /// Gets or sets the ID of the operation.
    /// </summary>
    [Required]
    public int Id { get; set; }
}
