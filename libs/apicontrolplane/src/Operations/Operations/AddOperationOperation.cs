namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for creating a new operation.
/// </summary>
public class AddOperationOperation : IResultOperation<AddOperationPostData, OperationModel>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IOperationFactory _OperationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="AddOperationOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// </exception>
    public AddOperationOperation(ILogger logger, IServiceFactory serviceFactory, IOperationFactory operationFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _OperationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (OperationModel, OperationError) Execute(AddOperationPostData input)
    {
        if (string.IsNullOrEmpty(input.Name)) return (null, new("{0} cannot be null or empty", nameof(input.Name)));
        if (string.IsNullOrEmpty(input.ServiceName)) return (null, new("{0} cannot be null or empty", nameof(input.ServiceName)));

        _Logger.Information("AddOperation, Name = {0}, ServiceName = {1}, IsEnabled = {2}", input.Name, input.ServiceName, input.IsEnabled);

        var service = _ServiceFactory.GetByName(input.ServiceName);
        if (service == null) return (null, new(ApiControlPlaneErrors.UnknownService, input.ServiceName));

        if (_OperationFactory.GetByName(service, input.Name) != null)
            return (null, new(ApiControlPlaneErrors.OperationAlreadyExists, input.Name));

        return (new(_OperationFactory.CreateNew(input.Name, service, input.IsEnabled)), null);
    }
}
