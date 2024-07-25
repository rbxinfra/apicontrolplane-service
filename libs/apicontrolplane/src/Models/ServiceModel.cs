namespace Roblox.ApiControlPlane.Models;

using System;

using Service.ApiControlPlane;

/// <summary>
/// The model for a <see cref="IService"/>
/// </summary>
public class ServiceModel
{
    /// <summary>
    /// Construct a new instance of <see cref="ServiceModel"/>
    /// </summary>
    /// <param name="service">The <see cref="IService"/></param>
    /// <exception cref="ArgumentNullException"><paramref name="service"/> cannot be null.</exception>
    public ServiceModel(IService service)
    {
        if (service == null) throw new ArgumentNullException(nameof(service));

        Id = service.ID;
        Name = service.Name;
        IsEnabled = service.IsEnabled;
        Created = service.Created;
        Updated = service.Updated;
    }

    /// <summary>
    /// Gets or sets the ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the value that determines if this service is enabled or not.
    /// </summary>
    public bool IsEnabled { get; set; }

    /// <summary>
    /// Gets or sets the created date.
    /// </summary>
    public DateTime Created { get; set; }

    /// <summary>
    /// Gets or sets the updated date.
    /// </summary>
    public DateTime Updated { get; set; }
}
