namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for updating a service.
/// </summary>
public class UpdateServiceOperation : IResultOperation<UpdateServicePostData, ServiceModel>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;

    /// <summary>
    /// Construct a new instance of <see cref="UpdateServiceOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// </exception>
    public UpdateServiceOperation(ILogger logger, IServiceFactory serviceFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ServiceModel, OperationError) Execute(UpdateServicePostData input)
    {
        var service = _ServiceFactory.GetByID(input.Id);
        if (service == null) return (null, new(ApiControlPlaneErrors.UnknownService, input.Id));

        _Logger.Information("UpdateService, ID = {0}", input.Id);

        if (!string.IsNullOrEmpty(input.Name) && input.Name != service.Name)
        {
            if (_ServiceFactory.GetByName(input.Name) != null) return (null, new(ApiControlPlaneErrors.ServiceAlreadyExists, input.Name));

            service.Name = input.Name;

            _Logger.Information("UpdateService: New Name = {0}", input.Name);
        }

        if (input.IsEnabled)
            service.Enable();
        else
            service.Disable();

        return (new(service), null);
    }
}
