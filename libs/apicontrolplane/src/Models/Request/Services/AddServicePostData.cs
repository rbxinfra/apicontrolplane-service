namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post data for the add service operation.
/// </summary>
public class AddServicePostData
{
    /// <summary>
    /// Gets or sets the note of the service
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    [Required]
    [DefaultValue(true)]
    public bool IsEnabled { get; set; } = true;
}
