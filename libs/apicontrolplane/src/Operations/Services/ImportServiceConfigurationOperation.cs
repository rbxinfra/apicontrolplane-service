namespace Roblox.ApiControlPlane;

using System;
using System.Linq;
using System.Collections.Generic;

using EventLog;
using Operations;
using Api.ControlPlane;
using Service.ApiControlPlane;

using Models;

/// <summary>
/// Operation for importing a service.
/// </summary>
public class ImportServiceConfigurationOperation : IResultOperation<ImportServiceConfigurationPostData, ServiceRegistrationModel>
{
    private readonly ILogger _Logger;
    private readonly ISettings _Settings;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IOperationFactory _OperationFactory;
    private readonly IApiClientFactory _ApiClientFactory;
    private readonly IServiceRegistrationFactory _ServiceRegistrationFactory;
    private readonly IServiceAuthorizationFactory _ServiceAuthorizationFactory;
    private readonly IOperationAuthorizationFactory _OperationAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="ImportServiceConfigurationOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="settings">The <see cref="ISettings"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <param name="serviceRegistrationFactory">The <see cref="IServiceRegistrationFactory"/></param>
    /// <param name="serviceAuthorizationFactory">The <see cref="IServiceAuthorizationFactory"/></param>
    /// <param name="operationAuthorizationFactory">The <see cref="IOperationAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="settings"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// - <paramref name="serviceRegistrationFactory"/> cannot be null.
    /// - <paramref name="serviceAuthorizationFactory"/> cannot be null.
    /// - <paramref name="operationAuthorizationFactory"/> cannot be null.
    /// </exception>
    public ImportServiceConfigurationOperation(
        ILogger logger,
        ISettings settings,
        IServiceFactory serviceFactory,
        IOperationFactory operationFactory,
        IApiClientFactory apiClientFactory,
        IServiceRegistrationFactory serviceRegistrationFactory,
        IServiceAuthorizationFactory serviceAuthorizationFactory,
        IOperationAuthorizationFactory operationAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _OperationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
        _ServiceRegistrationFactory = serviceRegistrationFactory ?? throw new ArgumentNullException(nameof(serviceRegistrationFactory));
        _ServiceAuthorizationFactory = serviceAuthorizationFactory ?? throw new ArgumentNullException(nameof(serviceAuthorizationFactory));
        _OperationAuthorizationFactory = operationAuthorizationFactory ?? throw new ArgumentNullException(nameof(operationAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ServiceRegistrationModel, OperationError) Execute(ImportServiceConfigurationPostData input)
    {
        if (!_Settings.ImportServiceConfigurationEnabled) return (null, null);

        if (string.IsNullOrEmpty(input.ServiceName)) return (null, new("{0} cannot be null or empty", nameof(input.ServiceName)));
        if (input.Operations.Length == 0) return (null, new("{0} cannot be empty", nameof(input.Operations)));

        _Logger.Information("ImportServiceConfiguration, ServiceName = {0}", input.ServiceName);

        var service = _ServiceFactory.GetByName(input.ServiceName) ?? 
                      _ServiceFactory.CreateNew(input.ServiceName, true);

        var existingOperations = _OperationFactory.GetAllByService(service);
        var operationsNeedingDeletion = existingOperations.Where(x => !input.Operations.Any(y => y == x.Name));

        foreach (var operation in operationsNeedingDeletion)
        {
            _Logger.Warning("Import does not define operation [{0}.{1}] anymore, deleting!", service.Name, operation.Name);

            var operationAuthorizations = _OperationAuthorizationFactory.GetAllByOperation(operation);

            // Service does this automatically, but DB does not, and will error with conflicts.
            foreach (var authorization in operationAuthorizations)
                authorization.Delete();

            operation.Delete();
        }

        foreach (var operationName in input.Operations)
        {
            if (string.IsNullOrEmpty(operationName))
                return (null, new("[config.Operations[]] cannot be null or empty!", nameof(input.Operations)));

            _ = _OperationFactory.GetByName(service, operationName) ??
                _OperationFactory.CreateNew(operationName, service, true);
        }

        if (_Settings.ImportClientAuthorizationsEnabled && input.ApiClients != null)
        {
            var allOperationNames = new HashSet<string>(input.Operations);

            foreach (var apiClientModel in input.ApiClients)
            {
                if (string.IsNullOrEmpty(apiClientModel.Note))
                    return (null, new("[config.ApiClients[].Note] cannot be null or empty!", nameof(input.Operations)));

                if (apiClientModel.AuthorizedOperations.Length == 0)
                {
                    _Logger.Warning("Skipping API Client [{0}] because it has no authorized operations", apiClientModel.Note);

                    continue;
                }

                var apiClient = _ApiClientFactory.GetByNote(apiClientModel.Note) ?? 
                                _ApiClientFactory.CreateNew(Guid.NewGuid(), apiClientModel.Note, true);

                if (!allOperationNames.SetEquals(apiClientModel.AuthorizedOperations))
                {
                    // Doesn't mean invalid operation names.
                    var nonExistingOperations = apiClientModel.AuthorizedOperations.Where(
                        x => !allOperationNames.Any(y => x == y)
                    );

                    if (nonExistingOperations.Any())
                    {
                        _Logger.Warning(
                            "Skipping API Client [{0}] because it contains authorized operations not defined in the configuration [{1}]", 
                            apiClientModel.Note, 
                            string.Join(", ", nonExistingOperations)
                        );
                        
                        continue;
                    }

                    _ = _ServiceAuthorizationFactory.GetByApiClientAndService(apiClient, service) ??
                        _ServiceAuthorizationFactory.CreateNew(apiClient, service, AuthorizationTypeEnum.Partial);
                    

                    foreach (var operationName in apiClientModel.AuthorizedOperations)
                    {
                        if (string.IsNullOrEmpty(operationName))
                        {
                            _Logger.Error("[config.ApiClients[].AuthorizedOperations[]] cannot be null or empty! Skipping...");

                            continue;
                        }

                        var operation = _OperationFactory.GetByName(service, operationName);
                        _ = _OperationAuthorizationFactory.GetByApiClientAndOperation(apiClient, operation) ??
                        _OperationAuthorizationFactory.CreateNew(apiClient, operation, AuthorizationTypeEnum.Full);
                    }
                }
                else
                {
                    _ = _ServiceAuthorizationFactory.GetByApiClientAndService(apiClient, service) ?? 
                        _ServiceAuthorizationFactory.CreateNew(apiClient, service, AuthorizationTypeEnum.Full);
                }
            }
        }

        var serviceRegistration = _ServiceRegistrationFactory.GetServiceRegistration(input.ServiceName);

        return (serviceRegistration != null ? new(serviceRegistration) : null, null);
    }
}
