namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;
using Service.ApiControlPlane;

using Models;

/// <summary>
/// Operation for creating a new operation authorization.
/// </summary>
public class AddOperationAuthorizationOperation : IResultOperation<AddOperationAuthorizationPostData, OperationAuthorizationModel>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IOperationFactory _OperationFactory;
    private readonly IApiClientFactory _ApiClientFactory;
    private readonly IServiceAuthorizationFactory _ServiceAuthorizationFactory;
    private readonly IOperationAuthorizationFactory _OperationAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="AddOperationAuthorizationOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <param name="serviceAuthorizationFactory">The <see cref="IServiceAuthorizationFactory"/></param>
    /// <param name="operationAuthorizationFactory">The <see cref="IOperationAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// - <paramref name="serviceAuthorizationFactory"/> cannot be null.
    /// - <paramref name="operationAuthorizationFactory"/> cannot be null.
    /// </exception>
    public AddOperationAuthorizationOperation(
        ILogger logger, 
        IServiceFactory serviceFactory, 
        IOperationFactory operationFactory,
        IApiClientFactory apiClientFactory,
        IServiceAuthorizationFactory serviceAuthorizationFactory,
        IOperationAuthorizationFactory operationAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _OperationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
        _ServiceAuthorizationFactory = serviceAuthorizationFactory ?? throw new ArgumentNullException(nameof(serviceAuthorizationFactory));
        _OperationAuthorizationFactory = operationAuthorizationFactory ?? throw new ArgumentNullException(nameof(operationAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (OperationAuthorizationModel, OperationError) Execute(AddOperationAuthorizationPostData input)
    {
        if (string.IsNullOrEmpty(input.ServiceName)) return (null, new("{0} cannot be null or empty", nameof(input.ServiceName)));
        if (string.IsNullOrEmpty(input.OperationName)) return (null, new("{0} cannot be null or empty", nameof(input.OperationName)));

        _Logger.Information(
            "AddOperationAuthorization, ServiceName = {0}, OperationName = {1}, Key = {2}, AuthorizationType = {3}", 
            input.ServiceName, 
            input.OperationName,
            input.Key, 
            input.AuthorizationType
        );

        var service = _ServiceFactory.GetByName(input.ServiceName);
        if (service == null) return (null, new(ApiControlPlaneErrors.UnknownService, input.ServiceName));

        var operation = _OperationFactory.GetByName(service, input.OperationName);
        if (operation == null) return (null, new(ApiControlPlaneErrors.UnknownOperation, input.OperationName));

        var apiClient = _ApiClientFactory.GetByKey(input.Key);
        if (apiClient == null) return (null, new(ApiControlPlaneErrors.UnknownClient, input.Key));

        var serviceAuthorization = _ServiceAuthorizationFactory.GetByApiClientAndService(apiClient, service);
        if (serviceAuthorization == null && input.AuthorizationType != AuthorizationType.None)
        {
            _Logger.Warning("AddOperationAuthorization: Creating new partial service authorization!");

            _ServiceAuthorizationFactory.CreateNew(apiClient, service, AuthorizationTypeEnum.Partial);
        }

        var operationAuthorization = _OperationAuthorizationFactory.GetByApiClientAndOperation(apiClient, operation);
        if (operationAuthorization != null)
        {
            _Logger.Warning("AddOperationAuthorization: Already exists, updating authorization type!");

            if (operationAuthorization.AuthorizationType == (AuthorizationTypeEnum)input.AuthorizationType)
                return (new(operationAuthorization), null);

            if (input.AuthorizationType == AuthorizationType.None)
            {
                _Logger.Warning("AddOperationAuthorization: Authorization type is None, deleting authorization type!");

                operationAuthorization.Delete();

                return (null, null);
            }

            operationAuthorization.AuthorizationType = (AuthorizationTypeEnum)input.AuthorizationType;

            operationAuthorization.Save();

            return (new(operationAuthorization), null);
        }

        if (input.AuthorizationType == AuthorizationType.None)
            return (null, new("Cannot create operation authorization type with None"));

        return (new(_OperationAuthorizationFactory.CreateNew(apiClient, operation, (AuthorizationTypeEnum)input.AuthorizationType)), null);
    }
}
