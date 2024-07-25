namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;
using Service.ApiControlPlane;

using Models;
using System.Threading.Tasks;

/// <summary>
/// Operation for creating a new service authorization.
/// </summary>
public class AddServiceAuthorizationOperation : IResultOperation<AddServiceAuthorizationPostData, ServiceAuthorizationModel>
{
    private readonly ILogger _Logger;
    private readonly ISettings _Settings;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IOperationFactory _OperationFactory;
    private readonly IApiClientFactory _ApiClientFactory;
    private readonly IServiceAuthorizationFactory _ServiceAuthorizationFactory;
    private readonly IOperationAuthorizationFactory _OperationAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="AddServiceAuthorizationOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="settings">The <see cref="ISettings"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <param name="serviceAuthorizationFactory">The <see cref="IServiceAuthorizationFactory"/></param>
    /// <param name="operationAuthorizationFactory">The <see cref="IOperationAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="settings"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// - <paramref name="serviceAuthorizationFactory"/> cannot be null.
    /// - <paramref name="operationAuthorizationFactory"/> cannot be null.
    /// </exception>
    public AddServiceAuthorizationOperation(
        ILogger logger, 
        ISettings settings,
        IServiceFactory serviceFactory, 
        IOperationFactory operationFactory,
        IApiClientFactory apiClientFactory,
        IServiceAuthorizationFactory serviceAuthorizationFactory,
        IOperationAuthorizationFactory operationAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _Settings = settings ?? throw new ArgumentNullException(nameof(settings));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _OperationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
        _ServiceAuthorizationFactory = serviceAuthorizationFactory ?? throw new ArgumentNullException(nameof(serviceAuthorizationFactory));
        _OperationAuthorizationFactory = operationAuthorizationFactory ?? throw new ArgumentNullException(nameof(operationAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ServiceAuthorizationModel, OperationError) Execute(AddServiceAuthorizationPostData input)
    {
        if (string.IsNullOrEmpty(input.ServiceName)) return (null, new("{0} cannot be null or empty", nameof(input.ServiceName)));

        _Logger.Information(
            "AddServiceAuthorization, ServiceName = {0}, Key = {1}, AuthorizationType = {2}", 
            input.ServiceName, 
            input.Key, 
            input.AuthorizationType
        );

        var service = _ServiceFactory.GetByName(input.ServiceName);
        if (service == null) return (null, new(ApiControlPlaneErrors.UnknownService, input.ServiceName));

        var apiClient = _ApiClientFactory.GetByKey(input.Key);
        if (apiClient == null) return (null, new(ApiControlPlaneErrors.UnknownClient, input.Key));

        var serviceAuthorization = _ServiceAuthorizationFactory.GetByApiClientAndService(apiClient, service);
        if (serviceAuthorization != null)
        {
            _Logger.Warning("AddServiceAuthorization: Already exists, updating authorization type!");

            if (serviceAuthorization.AuthorizationType == (AuthorizationTypeEnum)input.AuthorizationType)
                return (new(serviceAuthorization), null);

            if (input.AuthorizationType == AuthorizationType.None)
            {
                _Logger.Warning("AddServiceAuthorization: Authorization type is None, deleting authorization type!");

                if (_Settings.RemoveServiceAuthorizationShouldDeleteOperationAuthorizations)
                    Task.Run(() => DoRemoveServiceAuthorization(serviceAuthorization));
                else
                    serviceAuthorization.Delete();

                return (null, null);
            }

            serviceAuthorization.AuthorizationType = (AuthorizationTypeEnum)input.AuthorizationType;

            serviceAuthorization.Save();

            return (new(serviceAuthorization), null);
        }

        if (input.AuthorizationType == AuthorizationType.None) 
            return (null, new("Cannot create service authorization type with None"));

        return (new(_ServiceAuthorizationFactory.CreateNew(apiClient, service, (AuthorizationTypeEnum)input.AuthorizationType)), null);
    }

    private void DoRemoveServiceAuthorization(IServiceAuthorization serviceAuthorization)
    {
        _Logger.Information(
            "AddServiceAuthorization: Deleting operation authorizations for service and client"
        );

        var operations = _OperationFactory.GetAllByService(serviceAuthorization.Service);

        foreach (var operation in operations)
        {
            var operationAuthorizations = _OperationAuthorizationFactory.GetAllByOperation(operation);
            foreach (var operationAuthorization in operationAuthorizations)
                if (operationAuthorization.ApiClient.ID == serviceAuthorization.ApiClient.ID)
                    operationAuthorization.Delete();
        }

        serviceAuthorization.Delete();
    }
}
