namespace Roblox.ApiControlPlane;

using System;
using System.Linq;
using System.Collections.Generic;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting operations by service paged.
/// </summary>
public class GetOperationsByServicePagedOperation : IResultOperation<GetOperationsByServicePagedRequest, ICollection<OperationModel>>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IOperationFactory _OperationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetServicesPagedOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// </exception>
    public GetOperationsByServicePagedOperation(ILogger logger, IServiceFactory serviceFactory, IOperationFactory operationFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _OperationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ICollection<OperationModel>, OperationError) Execute(GetOperationsByServicePagedRequest input)
    {
        if (string.IsNullOrEmpty(input.ServiceName)) return (null, new("{0} cannot be null or empty", nameof(input.ServiceName)));
        if (input.StartRowIndex < 1) return (null, new("{0} must be greater than 0", nameof(input.StartRowIndex)));
        if (input.MaximumRows < 1) return (null, new("{0} must be greater than 0", nameof(input.MaximumRows)));

        _Logger.Information(
            "GetOperationsByServicePaged, ServiceName = {0}, StartRowIndex = {0}, MaximumRows = {1}",
            input.ServiceName,
            input.StartRowIndex, 
            input.MaximumRows
        );

        var service = _ServiceFactory.GetByName(input.ServiceName);
        if (service == null) return (null, new(ApiControlPlaneErrors.UnknownService, input.ServiceName));

        return (_OperationFactory.GetAllByService_Paged(
            service,
            input.StartRowIndex, 
            input.MaximumRows)
            .Select(operation => new OperationModel(operation)).ToArray(), 
        null);
    }
}
