namespace Roblox.ApiControlPlane;

using System;
using System.Linq;
using System.Collections.Generic;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting services paged.
/// </summary>
public class GetServicesPagedOperation : IResultOperation<GetServicesPagedRequest, ICollection<ServiceModel>>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetServicesPagedOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// </exception>
    public GetServicesPagedOperation(ILogger logger, IServiceFactory serviceFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ICollection<ServiceModel>, OperationError) Execute(GetServicesPagedRequest input)
    {
        if (input.StartRowIndex < 1) return (null, new("{0} must be greater than 0", nameof(input.StartRowIndex)));
        if (input.MaximumRows < 1) return (null, new("{0} must be greater than 0", nameof(input.MaximumRows)));

        _Logger.Information("GetServicesPaged, StartRowIndex = {0}, MaximumRows = {1}", input.StartRowIndex, input.MaximumRows);

        return (_ServiceFactory.GetAll_Paged(input.StartRowIndex, input.MaximumRows).Select(service => new ServiceModel(service)).ToArray(), null);
    }
}
