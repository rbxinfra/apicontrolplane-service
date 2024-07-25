namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post data for the update operation operation.
/// </summary>
public class UpdateOperationPostData
{
    /// <summary>
    /// Gets or sets the ID of the operation
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name of the operation
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the name of the service
    /// </summary>
    public string ServiceName { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    [DefaultValue(true)]
    public bool IsEnabled { get; set; } = true;
}
