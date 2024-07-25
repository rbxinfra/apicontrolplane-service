namespace Roblox.ApiControlPlane;

using System;
using System.Linq;
using System.Collections.Generic;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting operation authorizations by operation paged.
/// </summary>
public class GetOperationAuthorizationsByOperationPagedOperation 
    : IResultOperation<GetOperationAuthorizationsByOperationPagedRequest, ICollection<OperationAuthorizationModel>>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IOperationFactory _OperationFactory;
    private readonly IOperationAuthorizationFactory _OperationAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetOperationAuthorizationsByOperationPagedOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <param name="serviceAuthorizationFactory">The <see cref="IOperationAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// - <paramref name="serviceAuthorizationFactory"/> cannot be null.
    /// </exception>
    public GetOperationAuthorizationsByOperationPagedOperation(
        ILogger logger,
        IServiceFactory serviceFactory,
        IOperationFactory operationFactory,
        IOperationAuthorizationFactory serviceAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _OperationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
        _OperationAuthorizationFactory = serviceAuthorizationFactory ?? throw new ArgumentNullException(nameof(serviceAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ICollection<OperationAuthorizationModel>, OperationError) Execute(GetOperationAuthorizationsByOperationPagedRequest input)
    {
        if (string.IsNullOrEmpty(input.ServiceName)) return (null, new("{0} cannot be null or empty", nameof(input.ServiceName)));
        if (string.IsNullOrEmpty(input.OperationName)) return (null, new("{0} cannot be null or empty", nameof(input.OperationName)));
        if (input.StartRowIndex < 1) return (null, new("{0} must be greater than 0", nameof(input.StartRowIndex)));
        if (input.MaximumRows < 1) return (null, new("{0} must be greater than 0", nameof(input.MaximumRows)));

        _Logger.Information(
            "GetOperationAuthorizationsByOperationPaged, ServiceName = {0}, OperationName = {1}, StartRowIndex = {2}, MaximumRows = {3}",
            input.ServiceName,
            input.OperationName,
            input.StartRowIndex, 
            input.MaximumRows
        );

        var service = _ServiceFactory.GetByName(input.ServiceName);
        if (service == null) return (null, new(ApiControlPlaneErrors.UnknownService, input.ServiceName));

        var operation = _OperationFactory.GetByName(service, input.OperationName);
        if (operation == null) return (null, new(ApiControlPlaneErrors.UnknownOperation, input.OperationName));

        return (_OperationAuthorizationFactory.GetAllByOperation_Paged(
            operation,
            input.StartRowIndex, 
            input.MaximumRows)
            .Select(authorization => new OperationAuthorizationModel(authorization)).ToArray(), 
        null);
    }
}
