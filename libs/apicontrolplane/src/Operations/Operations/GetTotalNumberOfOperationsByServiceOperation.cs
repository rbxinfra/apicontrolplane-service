namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting total number of operations by service.
/// </summary>
public class GetTotalNumberOfOperationsByServiceOperation : IResultOperation<GetTotalNumberOfOperationsByServiceRequest, int>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IOperationFactory _OperationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetTotalNumberOfOperationsByServiceOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// </exception>
    public GetTotalNumberOfOperationsByServiceOperation(ILogger logger, IServiceFactory serviceFactory, IOperationFactory operationFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _OperationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
    }

    /// <inheritdoc cref="IOperation.Execute"/>
    public (int, OperationError) Execute(GetTotalNumberOfOperationsByServiceRequest input)
    {
        if (string.IsNullOrEmpty(input.ServiceName)) return (default, new("{0} cannot be null or empty", nameof(input.ServiceName)));

        _Logger.Information("GetTotalNumberOfOperationsByService, ServiceName = {0}", input.ServiceName);

        var service = _ServiceFactory.GetByName(input.ServiceName);
        if (service == null) return (default, new(ApiControlPlaneErrors.UnknownService, input.ServiceName));

        return (_OperationFactory.GetTotalNumberByService(service), null);
    }
}
