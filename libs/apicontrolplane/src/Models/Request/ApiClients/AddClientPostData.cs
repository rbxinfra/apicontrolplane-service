namespace Roblox.ApiControlPlane.Models;

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post data for the add client operation.
/// </summary>
public class AddClientPostData
{
    /// <summary>
    /// Gets or sets the key of the client
    /// </summary>
    public Guid? Key { get; set; }

    /// <summary>
    /// Gets or sets the note of the client
    /// </summary>
    [Required]
    public string Note { get; set; }

    /// <summary>
    /// Gets or sets the validity.
    /// </summary>
    [Required]
    [DefaultValue(true)]
    public bool IsValid { get; set; } = true;
}
