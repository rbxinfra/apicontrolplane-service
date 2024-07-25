namespace Roblox.ApiControlPlane.Models;

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post data for the update client operation.
/// </summary>
public class UpdateClientPostData
{
    /// <summary>
    /// Gets or sets the ID of the client
    /// </summary>
    [Required]
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the key of the client
    /// </summary>
    public Guid? Key { get; set; }

    /// <summary>
    /// Gets or sets the note of the client
    /// </summary>
    public string Note { get; set; }

    /// <summary>
    /// Gets or sets the validity.
    /// </summary>
    [DefaultValue(true)]
    public bool IsValid { get; set; } = true;
}
