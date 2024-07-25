namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post data for the update service operation.
/// </summary>
public class UpdateServicePostData
{
    /// <summary>
    /// Gets or sets the ID of the service
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the note of the service
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    [DefaultValue(true)]
    public bool IsEnabled { get; set; } = true;
}
