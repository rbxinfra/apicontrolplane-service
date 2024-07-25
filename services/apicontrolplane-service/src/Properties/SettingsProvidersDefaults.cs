namespace Roblox.ApiControlPlane.Service;

/// <summary>
/// Default details for the settings providers.
/// </summary>
internal static class SettingsProvidersDefaults
{
    /// <summary>
    /// The path prefix for the web platform.
    /// </summary>
    public const string ProviderPathPrefix = "services";

    /// <summary>
    /// The path to the apicontrolplane service settings.
    /// </summary>
    public const string ApiControlPlaneSettingsPath = $"{ProviderPathPrefix}/apicontrolplane-service";
}
