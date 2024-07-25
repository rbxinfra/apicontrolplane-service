namespace Roblox.ApiControlPlane.Service;

using EventLog;
using Configuration;
using Web.Framework.Services;

using static SettingsProvidersDefaults;

internal class Settings : BaseSettingsProvider<Settings>, IServiceSettings, ISettings
{
    /// <inheritdoc cref="IVaultProvider.Path"/>
    protected override string ChildPath => ApiControlPlaneSettingsPath;

    /// <inheritdoc cref="IServiceSettings.ApiKey"/>
    public string ApiKey => GetOrDefault(nameof(ApiKey), string.Empty);

    /// <inheritdoc cref="IServiceSettings.LogLevel"/>
    public LogLevel LogLevel => GetOrDefault(nameof(ApiKey), LogLevel.Information);

    /// <inheritdoc cref="ISettings.RemoveServiceAuthorizationShouldDeleteOperationAuthorizations"/>
    public bool RemoveServiceAuthorizationShouldDeleteOperationAuthorizations => GetOrDefault(nameof(RemoveServiceAuthorizationShouldDeleteOperationAuthorizations), true);

    /// <inheritdoc cref="ISettings.ImportServiceConfigurationEnabled"/>
    public bool ImportServiceConfigurationEnabled => GetOrDefault(nameof(ImportServiceConfigurationEnabled), true);

    /// <inheritdoc cref="ISettings.ImportClientAuthorizationsEnabled"/>
    public bool ImportClientAuthorizationsEnabled => GetOrDefault(nameof(ImportClientAuthorizationsEnabled), true);
}
