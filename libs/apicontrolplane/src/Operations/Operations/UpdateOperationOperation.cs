namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for updating an operation.
/// </summary>
public class UpdateOperationOperation : IResultOperation<UpdateOperationPostData, OperationModel>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IOperationFactory _OperationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="UpdateOperationOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// </exception>
    public UpdateOperationOperation(ILogger logger, IServiceFactory serviceFactory, IOperationFactory operationFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _OperationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (OperationModel, OperationError) Execute(UpdateOperationPostData input)
    {
        var operation = _OperationFactory.GetByID(input.Id);
        if (operation == null) return (null, new(ApiControlPlaneErrors.UnknownOperation, input.Id));

        _Logger.Information("UpdateOperation, ID = {0}", input.Id);

        if (!string.IsNullOrEmpty(input.ServiceName) && input.ServiceName != operation.Service.Name)
        {
            var service = _ServiceFactory.GetByName(input.ServiceName);
            if (service == null) return (null, new(ApiControlPlaneErrors.UnknownService, input.ServiceName));

            operation.Service = service;

            _Logger.Information("UpdateOperation: New Service = {0}", service.Name);
        }

        if (!string.IsNullOrEmpty(input.Name) && input.Name != operation.Name)
        {
            if (_OperationFactory.GetByName(operation.Service, input.Name) != null) 
                return (null, new(ApiControlPlaneErrors.OperationAlreadyExists, operation.Service.Name, input.Name));

            operation.Name = input.Name;

            _Logger.Information("UpdateOperation: New Name = {0}", input.Name);
        }

        if (input.IsEnabled)
            operation.Enable();
        else
            operation.Disable();

        return (new(operation), null);
    }
}
