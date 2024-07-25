namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting total number of operation authorization by operation.
/// </summary>
public class GetTotalNumberOfOperationAuthorizationsByOperationOperation 
    : IResultOperation<GetTotalNumberOfOperationAuthorizationsByOperationRequest, int>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IOperationFactory _OperationFactory;
    private readonly IOperationAuthorizationFactory _OperationAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetTotalNumberOfOperationAuthorizationsByOperationOperation"/>
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
    public GetTotalNumberOfOperationAuthorizationsByOperationOperation(
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

    /// <inheritdoc cref="IOperation.Execute"/>
    public (int, OperationError) Execute(GetTotalNumberOfOperationAuthorizationsByOperationRequest input)
    {
        if (string.IsNullOrEmpty(input.ServiceName)) return (default, new("{0} cannot be null or empty", nameof(input.ServiceName)));
        if (string.IsNullOrEmpty(input.OperationName)) return (default, new("{0} cannot be null or empty", nameof(input.OperationName)));

        _Logger.Information(
            "GetTotalNumberOfOperationAuthorizationsByOperation, ServiceName = {0}, OperationName = {1}", 
            input.ServiceName,
            input.OperationName
        );

        var service = _ServiceFactory.GetByName(input.ServiceName);
        if (service == null) return (default, new(ApiControlPlaneErrors.UnknownService, input.ServiceName));

        var operation = _OperationFactory.GetByName(service, input.OperationName);
        if (operation == null) return (default, new(ApiControlPlaneErrors.UnknownOperation, input.OperationName));

        return (_OperationAuthorizationFactory.GetTotalNumberByOperation(operation), null);
    }
}
