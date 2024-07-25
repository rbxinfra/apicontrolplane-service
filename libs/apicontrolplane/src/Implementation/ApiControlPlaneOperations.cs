namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Api.ControlPlane;

/// <summary>
/// Default implementation of <see cref="IApiControlPlaneOperations"/>
/// </summary>
public class ApiControlPlaneOperations : IApiControlPlaneOperations
{
    #region ApiClients

    /// <inheritdoc cref="IApiControlPlaneOperations.AddClientOperation"/>
    public AddClientOperation AddClientOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.DuplicateClientOperation"/>
    public DuplicateClientOperation DuplicateClientOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetClientByIDOperation"/>
    public GetClientByIDOperation GetClientByIDOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetClientByKeyOperation"/>
    public GetClientByKeyOperation GetClientByKeyOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetClientByNoteOperation"/>
    public GetClientByNoteOperation GetClientByNoteOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetClientsPagedOperation"/>
    public GetClientsPagedOperation GetClientsPagedOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetTotalNumberOfClientsOperation"/>
    public GetTotalNumberOfClientsOperation GetTotalNumberOfClientsOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.RemoveClientOperation"/>
    public RemoveClientOperation RemoveClientOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.UpdateClientOperation"/>
    public UpdateClientOperation UpdateClientOperation { get; }

    #endregion

    #region Services

    /// <inheritdoc cref="IApiControlPlaneOperations.AddServiceOperation"/>
    public AddServiceOperation AddServiceOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetServiceByIDOperation"/>
    public GetServiceByIDOperation GetServiceByIDOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetServiceByNameOperation"/>
    public GetServiceByNameOperation GetServiceByNameOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetServicesPagedOperation"/>
    public GetServicesPagedOperation GetServicesPagedOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetTotalNumberOfServicesOperation"/>
    public GetTotalNumberOfServicesOperation GetTotalNumberOfServicesOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.RemoveServiceOperation"/>
    public RemoveServiceOperation RemoveServiceOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.UpdateServiceOperation"/>
    public UpdateServiceOperation UpdateServiceOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetServiceRegistrationOperation"/>
    public GetServiceRegistrationOperation GetServiceRegistrationOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.ImportServiceConfigurationOperation"/>
    public ImportServiceConfigurationOperation ImportServiceConfigurationOperation { get; }

    #endregion

    #region Operations

    /// <inheritdoc cref="IApiControlPlaneOperations.AddOperationOperation"/>
    public AddOperationOperation AddOperationOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetOperationByIDOperation"/>
    public GetOperationByIDOperation GetOperationByIDOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetOperationByServiceOperation"/>
    public GetOperationByServiceOperation GetOperationByServiceOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetOperationsByServicePagedOperation"/>
    public GetOperationsByServicePagedOperation GetOperationsByServicePagedOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetTotalNumberOfOperationsByServiceOperation"/>
    public GetTotalNumberOfOperationsByServiceOperation GetTotalNumberOfOperationsByServiceOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.RemoveOperationOperation"/>
    public RemoveOperationOperation RemoveOperationOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.UpdateOperationOperation"/>
    public UpdateOperationOperation UpdateOperationOperation { get; }

    #endregion

    #region ServiceAuthorizations

    /// <inheritdoc cref="IApiControlPlaneOperations.AddServiceAuthorizationOperation"/>
    public AddServiceAuthorizationOperation AddServiceAuthorizationOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetServiceAuthorizationsByClientPagedOperation"/>
    public GetServiceAuthorizationsByClientPagedOperation GetServiceAuthorizationsByClientPagedOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetServiceAuthorizationsByServicePagedOperation"/>
    public GetServiceAuthorizationsByServicePagedOperation GetServiceAuthorizationsByServicePagedOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetTotalNumberOfServiceAuthorizationsByClientOperation"/>
    public GetTotalNumberOfServiceAuthorizationsByClientOperation GetTotalNumberOfServiceAuthorizationsByClientOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetTotalNumberOfServiceAuthorizationsByServiceOperation"/>
    public GetTotalNumberOfServiceAuthorizationsByServiceOperation GetTotalNumberOfServiceAuthorizationsByServiceOperation { get; }

    #endregion

    #region OperationAuthorizations

    /// <inheritdoc cref="IApiControlPlaneOperations.AddOperationAuthorizationOperation"/>
    public AddOperationAuthorizationOperation AddOperationAuthorizationOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetOperationAuthorizationsByClientPagedOperation"/>
    public GetOperationAuthorizationsByClientPagedOperation GetOperationAuthorizationsByClientPagedOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetOperationAuthorizationsByOperationPagedOperation"/>
    public GetOperationAuthorizationsByOperationPagedOperation GetOperationAuthorizationsByOperationPagedOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetTotalNumberOfOperationAuthorizationsByClientOperation"/>
    public GetTotalNumberOfOperationAuthorizationsByClientOperation GetTotalNumberOfOperationAuthorizationsByClientOperation { get; }

    /// <inheritdoc cref="IApiControlPlaneOperations.GetTotalNumberOfOperationAuthorizationsByOperationOperation"/>
    public GetTotalNumberOfOperationAuthorizationsByOperationOperation GetTotalNumberOfOperationAuthorizationsByOperationOperation { get; }

    #endregion

    /// <summary>
    /// Construct a new instance of <see cref="ApiControlPlaneOperations"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="settings">The <see cref="ISettings"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <param name="serviceRegistrationFactory">The <see cref="IServiceRegistrationFactory"/></param>
    /// <param name="serviceAuthorizationFactory">The <see cref="IServiceAuthorizationFactory"/></param>
    /// <param name="operationAuthorizationFactory">The <see cref="IOperationAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="settings"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// - <paramref name="serviceRegistrationFactory"/> cannot be null.
    /// - <paramref name="serviceAuthorizationFactory"/> cannot be null.
    /// - <paramref name="operationAuthorizationFactory"/> cannot be null.
    /// </exception>
    public ApiControlPlaneOperations(
        ILogger logger, 
        ISettings settings,
        IApiClientFactory apiClientFactory,
        IServiceFactory serviceFactory,
        IOperationFactory operationFactory,
        IServiceRegistrationFactory serviceRegistrationFactory,
        IServiceAuthorizationFactory serviceAuthorizationFactory,
        IOperationAuthorizationFactory operationAuthorizationFactory
    )
    {
        if (logger == null) throw new ArgumentNullException(nameof(logger));
        if (settings == null) throw new ArgumentNullException(nameof(settings));
        if (apiClientFactory == null) throw new ArgumentNullException(nameof(apiClientFactory));
        if (serviceFactory == null) throw new ArgumentNullException(nameof(serviceFactory));
        if (operationFactory == null) throw new ArgumentNullException(nameof(operationFactory));
        if (serviceRegistrationFactory == null) throw new ArgumentNullException(nameof(serviceRegistrationFactory));
        if (serviceAuthorizationFactory == null) throw new ArgumentNullException(nameof(serviceAuthorizationFactory));
        if (operationAuthorizationFactory == null) throw new ArgumentNullException(nameof(operationAuthorizationFactory));

        #region ApiClients

        AddClientOperation = new(logger, apiClientFactory);
        DuplicateClientOperation = new(logger, apiClientFactory, serviceAuthorizationFactory, operationAuthorizationFactory);
        GetClientByIDOperation = new(logger, apiClientFactory);
        GetClientByKeyOperation = new(logger, apiClientFactory);
        GetClientByNoteOperation = new(logger, apiClientFactory);
        GetClientsPagedOperation = new(logger, apiClientFactory);
        GetTotalNumberOfClientsOperation = new(logger, apiClientFactory);
        RemoveClientOperation = new(logger, apiClientFactory, serviceAuthorizationFactory, operationAuthorizationFactory);
        UpdateClientOperation = new(logger, apiClientFactory);

        #endregion

        #region Services

        AddServiceOperation = new(logger, serviceFactory);
        GetServiceByIDOperation = new(logger, serviceFactory);
        GetServiceByNameOperation = new(logger, serviceFactory);
        GetServicesPagedOperation = new(logger, serviceFactory);
        GetTotalNumberOfServicesOperation = new(logger, serviceFactory);
        RemoveServiceOperation = new(logger, serviceFactory, operationFactory, serviceAuthorizationFactory, operationAuthorizationFactory);
        UpdateServiceOperation = new(logger, serviceFactory);
        GetServiceRegistrationOperation = new(logger, serviceRegistrationFactory);
        ImportServiceConfigurationOperation = new(logger, settings, serviceFactory, operationFactory, apiClientFactory, serviceRegistrationFactory, serviceAuthorizationFactory, operationAuthorizationFactory);

        #endregion

        #region Operations

        AddOperationOperation = new(logger, serviceFactory, operationFactory);
        GetOperationByIDOperation = new(logger, operationFactory);
        GetOperationByServiceOperation = new(logger, serviceFactory, operationFactory);
        GetOperationsByServicePagedOperation = new(logger, serviceFactory, operationFactory);
        GetTotalNumberOfOperationsByServiceOperation = new(logger, serviceFactory, operationFactory);
        RemoveOperationOperation = new(logger, operationFactory, operationAuthorizationFactory);
        UpdateOperationOperation = new(logger, serviceFactory, operationFactory);

        #endregion

        #region ServiceAuthorizations

        AddServiceAuthorizationOperation = new(logger, settings, serviceFactory, operationFactory, apiClientFactory, serviceAuthorizationFactory, operationAuthorizationFactory);
        GetServiceAuthorizationsByClientPagedOperation = new(logger, apiClientFactory, serviceAuthorizationFactory);
        GetServiceAuthorizationsByServicePagedOperation = new(logger, serviceFactory, serviceAuthorizationFactory);
        GetTotalNumberOfServiceAuthorizationsByClientOperation = new(logger, apiClientFactory, serviceAuthorizationFactory);
        GetTotalNumberOfServiceAuthorizationsByServiceOperation = new(logger, serviceFactory, serviceAuthorizationFactory);

        #region ServiceAuthorizations

        AddServiceAuthorizationOperation = new(logger, settings, serviceFactory, operationFactory, apiClientFactory, serviceAuthorizationFactory, operationAuthorizationFactory);
        GetServiceAuthorizationsByClientPagedOperation = new(logger, apiClientFactory, serviceAuthorizationFactory);
        GetServiceAuthorizationsByServicePagedOperation = new(logger, serviceFactory, serviceAuthorizationFactory);
        GetTotalNumberOfServiceAuthorizationsByClientOperation = new(logger, apiClientFactory, serviceAuthorizationFactory);
        GetTotalNumberOfServiceAuthorizationsByServiceOperation = new(logger, serviceFactory, serviceAuthorizationFactory);

        #endregion
        #endregion

        #region OperationAuthorizations

        AddOperationAuthorizationOperation = new(logger, serviceFactory, operationFactory, apiClientFactory, serviceAuthorizationFactory, operationAuthorizationFactory);
        GetOperationAuthorizationsByClientPagedOperation = new(logger, apiClientFactory, operationAuthorizationFactory);
        GetOperationAuthorizationsByOperationPagedOperation = new(logger, serviceFactory, operationFactory, operationAuthorizationFactory);
        GetTotalNumberOfOperationAuthorizationsByClientOperation = new(logger, apiClientFactory, operationAuthorizationFactory);
        GetTotalNumberOfOperationAuthorizationsByOperationOperation = new(logger, serviceFactory, operationFactory, operationAuthorizationFactory);

        #endregion

    }
}
