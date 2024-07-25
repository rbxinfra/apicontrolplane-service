namespace Roblox.ApiControlPlane;

/// <summary>
/// Settings for API control plane code.
/// </summary>
public interface ISettings
{
    /// <summary>
    /// Determines if operation authorizations should be deleted as well
    /// as a service authorization.
    /// </summary>
    bool RemoveServiceAuthorizationShouldDeleteOperationAuthorizations { get; }

    /// <summary>
    /// Determines if the <see cref="ImportServiceConfigurationOperation"/>
    /// is enabled or not.
    /// </summary>
    bool ImportServiceConfigurationEnabled { get; }

    /// <summary>
    /// Determines if the import service configuration should import
    /// the client authorizations as well.
    /// </summary>
    bool ImportClientAuthorizationsEnabled { get; }
}
