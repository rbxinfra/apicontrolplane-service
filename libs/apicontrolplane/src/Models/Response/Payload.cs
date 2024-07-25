namespace Roblox.ApiControlPlane.Models;

/// <summary>
/// A model to wrap response data.
/// </summary>
/// <typeparam name="TData">The response data type.</typeparam>
public class Payload<TData>
{
    /// <summary>
    /// The response data.
    /// </summary>
    public TData Data { get; set; }
}
