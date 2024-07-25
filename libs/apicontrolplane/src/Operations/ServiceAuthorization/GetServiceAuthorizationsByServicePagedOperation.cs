namespace Roblox.ApiControlPlane;

using System;
using System.Linq;
using System.Collections.Generic;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting service authorizations by service paged.
/// </summary>
public class GetServiceAuthorizationsByServicePagedOperation 
    : IResultOperation<GetServiceAuthorizationsByServicePagedRequest, ICollection<ServiceAuthorizationModel>>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IServiceAuthorizationFactory _ServiceAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetServiceAuthorizationsByServicePagedOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="serviceAuthorizationFactory">The <see cref="IServiceAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="serviceAuthorizationFactory"/> cannot be null.
    /// </exception>
    public GetServiceAuthorizationsByServicePagedOperation(
        ILogger logger, 
        IServiceFactory serviceFactory, 
        IServiceAuthorizationFactory serviceAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _ServiceAuthorizationFactory = serviceAuthorizationFactory ?? throw new ArgumentNullException(nameof(serviceAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ICollection<ServiceAuthorizationModel>, OperationError) Execute(GetServiceAuthorizationsByServicePagedRequest input)
    {
        if (string.IsNullOrEmpty(input.ServiceName)) return (null, new("{0} cannot be null or empty", nameof(input.ServiceName)));
        if (input.StartRowIndex < 1) return (null, new("{0} must be greater than 0", nameof(input.StartRowIndex)));
        if (input.MaximumRows < 1) return (null, new("{0} must be greater than 0", nameof(input.MaximumRows)));

        _Logger.Information(
            "GetServiceAuthorizationsByServicePaged, ServiceName = {0}, StartRowIndex = {0}, MaximumRows = {1}",
            input.ServiceName,
            input.StartRowIndex, 
            input.MaximumRows
        );

        var service = _ServiceFactory.GetByName(input.ServiceName);
        if (service == null) return (null, new(ApiControlPlaneErrors.UnknownService, input.ServiceName));

        return (_ServiceAuthorizationFactory.GetAllByService_Paged(
            service,
            input.StartRowIndex, 
            input.MaximumRows)
            .Select(authorization => new ServiceAuthorizationModel(authorization)).ToArray(), 
        null);
    }
}
