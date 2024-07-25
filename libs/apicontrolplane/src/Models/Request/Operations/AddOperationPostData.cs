namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post data for the add operation operation.
/// </summary>
public class AddOperationPostData
{
    /// <summary>
    /// Gets or sets the name of the operation
    /// </summary>
    [Required]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the name of the service
    /// </summary>
    [Required]
    public string ServiceName { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    [Required]
    [DefaultValue(true)]
    public bool IsEnabled { get; set; } = true;
}
