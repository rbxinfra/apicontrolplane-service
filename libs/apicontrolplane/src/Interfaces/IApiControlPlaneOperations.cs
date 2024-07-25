namespace Roblox.ApiControlPlane;

/// <summary>
/// Operations accessor for API Control Plane
/// </summary>
public interface IApiControlPlaneOperations
{
    #region Api Clients

    /// <summary>
    /// Gets the add client operation.
    /// </summary>
    AddClientOperation AddClientOperation { get; }

    /// <summary>
    /// Gets the duplicate client operation.
    /// </summary>
    DuplicateClientOperation DuplicateClientOperation { get; }

    /// <summary>
    /// Gets the get client by id operation.
    /// </summary>
    GetClientByIDOperation GetClientByIDOperation { get; }

    /// <summary>
    /// Gets the get client by key operation.
    /// </summary>
    GetClientByKeyOperation GetClientByKeyOperation { get; }

    /// <summary>
    /// Gets the get client by note operation.
    /// </summary>
    GetClientByNoteOperation GetClientByNoteOperation { get; }

    /// <summary>
    /// Gets the clients paged operation.
    /// </summary>
    GetClientsPagedOperation GetClientsPagedOperation { get; }

    /// <summary>
    /// Gets the get total number of clients operation.
    /// </summary>
    GetTotalNumberOfClientsOperation GetTotalNumberOfClientsOperation { get; }

    /// <summary>
    /// Gets the remove client operation.
    /// </summary>
    RemoveClientOperation RemoveClientOperation { get; }

    /// <summary>
    /// Gets the update client operation.
    /// </summary>
    UpdateClientOperation UpdateClientOperation { get; }

    #endregion

    #region Services

    /// <summary>
    /// Gets the add service operation.
    /// </summary>
    AddServiceOperation AddServiceOperation { get; }

    /// <summary>
    /// Gets the get service by id operation.
    /// </summary>
    GetServiceByIDOperation GetServiceByIDOperation { get; }

    /// <summary>
    /// Gets the get service by name operation.
    /// </summary>
    GetServiceByNameOperation GetServiceByNameOperation { get; }

    /// <summary>
    /// Gets the services paged operation.
    /// </summary>
    GetServicesPagedOperation GetServicesPagedOperation { get; }

    /// <summary>
    /// Gets the get total number of services operation.
    /// </summary>
    GetTotalNumberOfServicesOperation GetTotalNumberOfServicesOperation { get; }

    /// <summary>
    /// Gets the remove service operation.
    /// </summary>
    RemoveServiceOperation RemoveServiceOperation { get; }

    /// <summary>
    /// Gets the update service operation.
    /// </summary>
    UpdateServiceOperation UpdateServiceOperation { get; }

    /// <summary>
    /// Gets the get service registration operation.
    /// </summary>
    GetServiceRegistrationOperation GetServiceRegistrationOperation { get; }

    /// <summary>
    /// Gets the import service configuration operation.
    /// </summary>
    ImportServiceConfigurationOperation ImportServiceConfigurationOperation { get; }

    #endregion

    #region Operations

    /// <summary>
    /// Gets the add operation operation.
    /// </summary>
    AddOperationOperation AddOperationOperation { get; }

    /// <summary>
    /// Gets the get operation by id operation.
    /// </summary>
    GetOperationByIDOperation GetOperationByIDOperation { get; }

    /// <summary>
    /// Gets the get operation by service operation.
    /// </summary>
    GetOperationByServiceOperation GetOperationByServiceOperation { get; }

    /// <summary>
    /// Gets the operations by service paged operation.
    /// </summary>
    GetOperationsByServicePagedOperation GetOperationsByServicePagedOperation { get; }

    /// <summary>
    /// Gets the get total number of operations by service operation.
    /// </summary>
    GetTotalNumberOfOperationsByServiceOperation GetTotalNumberOfOperationsByServiceOperation { get; }

    /// <summary>
    /// Gets the remove operation operation.
    /// </summary>
    RemoveOperationOperation RemoveOperationOperation { get; }

    /// <summary>
    /// Gets the update operation operation.
    /// </summary>
    UpdateOperationOperation UpdateOperationOperation { get; }

    #endregion

    #region ServiceAuthorizations

    /// <summary>
    /// Gets the add service authorization operation.
    /// </summary>
    AddServiceAuthorizationOperation AddServiceAuthorizationOperation { get; }

    /// <summary>
    /// Gets the service authorizations by client paged operation.
    /// </summary>
    GetServiceAuthorizationsByClientPagedOperation GetServiceAuthorizationsByClientPagedOperation { get; }

    /// <summary>
    /// Gets the service authorizations by service paged operation.
    /// </summary>
    GetServiceAuthorizationsByServicePagedOperation GetServiceAuthorizationsByServicePagedOperation { get; }

    /// <summary>
    /// Gets the get total number of service authorizations by client operation.
    /// </summary>
    GetTotalNumberOfServiceAuthorizationsByClientOperation GetTotalNumberOfServiceAuthorizationsByClientOperation { get; }

    /// <summary>
    /// Gets the get total number of service authorizations by service operation.
    /// </summary>
    GetTotalNumberOfServiceAuthorizationsByServiceOperation GetTotalNumberOfServiceAuthorizationsByServiceOperation { get; }

    #endregion

    #region OperationAuthorizations

    /// <summary>
    /// Gets the add operation authorization operation.
    /// </summary>
    AddOperationAuthorizationOperation AddOperationAuthorizationOperation { get; }

    /// <summary>
    /// Gets the operation authorizations by client paged operation.
    /// </summary>
    GetOperationAuthorizationsByClientPagedOperation GetOperationAuthorizationsByClientPagedOperation { get; }

    /// <summary>
    /// Gets the operation authorizations by operation paged operation.
    /// </summary>
    GetOperationAuthorizationsByOperationPagedOperation GetOperationAuthorizationsByOperationPagedOperation { get; }

    /// <summary>
    /// Gets the get total number of operation authorizations by client operation.
    /// </summary>
    GetTotalNumberOfOperationAuthorizationsByClientOperation GetTotalNumberOfOperationAuthorizationsByClientOperation { get; }

    /// <summary>
    /// Gets the get total number of operation authorizations by operation operation.
    /// </summary>
    GetTotalNumberOfOperationAuthorizationsByOperationOperation GetTotalNumberOfOperationAuthorizationsByOperationOperation { get; }

    #endregion
}
