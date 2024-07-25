namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting an operation by service.
/// </summary>
public class GetOperationByServiceOperation : IResultOperation<GetOperationByServiceRequest, OperationModel>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IOperationFactory _OperationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetOperationByServiceOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// </exception>
    public GetOperationByServiceOperation(ILogger logger, IServiceFactory serviceFactory, IOperationFactory operationFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _OperationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (OperationModel, OperationError) Execute(GetOperationByServiceRequest input)
    {
        if (string.IsNullOrEmpty(input.ServiceName)) return (null, new("{0} cannot be null or empty", nameof(input.ServiceName)));
        if (string.IsNullOrEmpty(input.Name)) return (null, new("{0} cannot be null or empty", nameof(input.Name)));

        _Logger.Information("GetOperationByService, ServiceName = {0}, Name = {1}", input.ServiceName, input.Name);

        var service = _ServiceFactory.GetByName(input.ServiceName);
        if (service == null) return (null, new(ApiControlPlaneErrors.UnknownService, input.ServiceName));

        var operation = _OperationFactory.GetByName(service, input.Name);

        return (service != null ? new(operation) : null, null);
    }
}
