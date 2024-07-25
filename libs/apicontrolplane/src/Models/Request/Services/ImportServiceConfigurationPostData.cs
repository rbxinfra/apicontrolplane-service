namespace Roblox.ApiControlPlane.Models;

using System.ComponentModel.DataAnnotations;

/// <summary>
/// Post data for the import service configuration operation.
/// </summary>
public class ImportServiceConfigurationPostData
{
    /// <summary>
    /// Api Client model but specifically for the import service configuration operation.
    /// </summary>
    public class ConfigurationApiClientModel
    {
        /// <summary>
        /// Gets or sets the API client note.
        /// </summary>
        [Required]
        public string Note { get; set; }

        /// <summary>
        /// Gets or sets the authorized operations.
        /// </summary>
        [Required]
        public string[] AuthorizedOperations { get; set; }
    }

    /// <summary>
    /// Gets or sets the name of the service.
    /// </summary>
    [Required]
    public string ServiceName { get; set; }

    /// <summary>
    /// Gets the list of operation names
    /// </summary>
    [Required]
    public string[] Operations { get; set; }

    /// <summary>
    /// Gets the list of api clients
    /// </summary>
    /// <remarks>
    /// This is only valid if <see cref="ISettings.ImportClientAuthorizationsEnabled"/> is true.
    /// </remarks>
    public ConfigurationApiClientModel[] ApiClients { get; set; }
}
