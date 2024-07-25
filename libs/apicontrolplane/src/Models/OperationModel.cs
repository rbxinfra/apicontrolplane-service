namespace Roblox.ApiControlPlane.Models;

using System;

using Service.ApiControlPlane;

/// <summary>
/// The model for a <see cref="IOperation"/>
/// </summary>
public class OperationModel
{
    /// <summary>
    /// Construct a new instance of <see cref="OperationModel"/>
    /// </summary>
    /// <param name="operation">The <see cref="IOperation"/></param>
    /// <exception cref="ArgumentNullException"><paramref name="operation"/> cannot be null.</exception>
    public OperationModel(IOperation operation)
    {
        if (operation == null) throw new ArgumentNullException(nameof(operation));

        Id = operation.ID;
        Name = operation.Name;
        ServiceId = operation.Service.ID;
        IsEnabled = operation.IsEnabled;
        Created = operation.Created;
        Updated = operation.Updated;
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
    /// Gets or sets the ID of the service.
    /// </summary>
    public int ServiceId { get; set; }

    /// <summary>
    /// Gets or sets the value that determines if this operation is enabled or not.
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
