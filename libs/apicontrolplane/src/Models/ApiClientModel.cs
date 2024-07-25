namespace Roblox.ApiControlPlane.Models;

using System;

using Service.ApiControlPlane;

/// <summary>
/// The model for a <see cref="IApiClient"/>
/// </summary>
public class ApiClientModel
{
    /// <summary>
    /// Construct a new instance of <see cref="ApiClientModel"/>
    /// </summary>
    /// <param name="apiClient">The <see cref="IApiClient"/></param>
    /// <exception cref="ArgumentNullException"><paramref name="apiClient"/> cannot be null.</exception>
    public ApiClientModel(IApiClient apiClient)
    {
        if (apiClient == null) throw new ArgumentNullException(nameof(apiClient));

        Id = apiClient.ID;
        Key = apiClient.ApiKey.ToString();
        Note = apiClient.Note;
        IsValid = apiClient.IsValid;
        Created = apiClient.Created;
        Updated = apiClient.Updated;
    }

    /// <summary>
    /// Gets or sets the ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the API key
    /// </summary>
    public string Key { get; set; }

    /// <summary>
    /// Gets or sets the note.
    /// </summary>
    public string Note { get; set; }

    /// <summary>
    /// Gets or sets the value that determines if this API key is enabled or not.
    /// </summary>
    public bool IsValid { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    public DateTime Updated { get; set; }
}
