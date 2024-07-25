namespace Roblox.ApiControlPlane.Service;

using System;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

using Roblox.Web.Framework.Services;
using Roblox.Web.Framework.Services.Http;

using Roblox.Api.ControlPlane;
using Roblox.Api.ControlPlane.Factories;
using Roblox.Service.ApiControlPlane;

/// <summary>
/// Startup class for apicontrolplane-service.
/// </summary>
public class Startup : HttpStartupBase
{
    /// <inheritdoc cref="StartupBase.Settings"/>
    protected override IServiceSettings Settings => ApiControlPlane.Service.Settings.Singleton;

    /// <inheritdoc cref="StartupBase.ConfigureServices(IServiceCollection)"/>
    public override void ConfigureServices(IServiceCollection services)
    {
        base.ConfigureServices(services);

        services.AddSingleton<ISettings>(ApiControlPlane.Service.Settings.Singleton);

        var apiClientFactory = new ApiClientFactory();
        var serviceFactory = new ServiceFactory();
        var operationFactory = new OperationFactory();
        var serviceAuthorizationFactory = new ServiceAuthorizationFactory();
        var operationAuthorizationFactory = new OperationAuthorizationFactory();

        services.AddSingleton<IApiClientFactory>(apiClientFactory);
        services.AddSingleton<IServiceFactory>(serviceFactory);
        services.AddSingleton<IOperationFactory>(operationFactory);
        services.AddSingleton<IServiceAuthorizationFactory>(serviceAuthorizationFactory);
        services.AddSingleton<IOperationAuthorizationFactory>(operationAuthorizationFactory);

        var serviceRegistrationFactory = new ServiceRegistrationFactory(
            serviceFactory,
            apiClientFactory,
            operationFactory,
            serviceAuthorizationFactory,
            operationAuthorizationFactory
        );

        services.RemoveAll(typeof(IServiceRegistrationFactory));

        services.AddSingleton<IServiceRegistrationFactory>(serviceRegistrationFactory);
        services.AddSingleton<IAuthorizationFactory, AuthorizationFactory>();

        services.AddTransient<IApiControlPlaneOperations, ApiControlPlaneOperations>();

    }

    /// <inheritdoc cref="StartupBase.ConfigureAuthority(IServiceProvider)"/>
    protected override IAuthority ConfigureAuthority(IServiceProvider services)
        => new Authority();
}
