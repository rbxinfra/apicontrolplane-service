namespace Roblox.ApiControlPlane;

using System;
using System.Threading.Tasks;

using EventLog;
using Operations;
using Api.ControlPlane;
using Service.ApiControlPlane;

using Models;

/// <summary>
/// Operation for removing a service.
/// </summary>
public class RemoveServiceOperation : IOperation<RemoveServicePostData>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;
    private readonly IOperationFactory _OperationFactory;
    private readonly IServiceAuthorizationFactory _ServiceAuthorizationFactory;
    private readonly IOperationAuthorizationFactory _OperationAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="RemoveServiceOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <param name="serviceAuthorizationFactory">The <see cref="IServiceAuthorizationFactory"/></param>
    /// <param name="operationAuthorizationFactory">The <see cref="IOperationAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// - <paramref name="serviceAuthorizationFactory"/> cannot be null.
    /// - <paramref name="operationAuthorizationFactory"/> cannot be null.
    /// </exception>
    public RemoveServiceOperation(
        ILogger logger,
        IServiceFactory serviceFactory,
        IOperationFactory operationFactory,
        IServiceAuthorizationFactory serviceAuthorizationFactory,
        IOperationAuthorizationFactory operationAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
        _OperationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
        _ServiceAuthorizationFactory = serviceAuthorizationFactory ?? throw new ArgumentNullException(nameof(serviceAuthorizationFactory));
        _OperationAuthorizationFactory = operationAuthorizationFactory ?? throw new ArgumentNullException(nameof(operationAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public OperationError Execute(RemoveServicePostData input)
    {
        _Logger.Information("RemoveService, ID = {1}", input.Id);

        var service = _ServiceFactory.GetByID(input.Id);
        if (service == null) return new(ApiControlPlaneErrors.UnknownService, input.Id);

        _Logger.Warning("RemoveService: Disabling service {0} before removing it!", service.Name);

        service.Disable();

        Task.Run(() => DoRemoveService(service));

        return null;
    }

    private void DoRemoveService(IService service)
    {
        _Logger.Information("RemoveService: Deleting authorizations and operations for Service '{0}'", service.Name);

        var operations = _OperationFactory.GetAllByService(service);

        foreach (var operation in operations)
        {
            var operationAuthorizations = _OperationAuthorizationFactory.GetAllByOperation(operation);
            foreach (var operationAuthorization in operationAuthorizations)
                operationAuthorization.Delete();

            operation.Delete();
        }

        var serviceAuthorizations = _ServiceAuthorizationFactory.GetAllByService(service);
        foreach (var serviceAuthorization in serviceAuthorizations)
            serviceAuthorization.Delete();

        service.Delete();
    }
}
