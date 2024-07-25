namespace Roblox.ApiControlPlane;

using System;
using System.Threading.Tasks;

using EventLog;
using Operations;
using Api.ControlPlane;
using Service.ApiControlPlane;

using Models;

using IOperation = Roblox.Service.ApiControlPlane.IOperation;

/// <summary>
/// Operation for removing a operation.
/// </summary>
public class RemoveOperationOperation : IOperation<RemoveOperationPostData>
{
    private readonly ILogger _Logger;
    private readonly IOperationFactory _OperationFactory;
    private readonly IOperationAuthorizationFactory _OperationAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="RemoveOperationOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <param name="operationAuthorizationFactory">The <see cref="IOperationAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// - <paramref name="operationAuthorizationFactory"/> cannot be null.
    /// </exception>
    public RemoveOperationOperation(
        ILogger logger,
        IOperationFactory operationFactory,
        IOperationAuthorizationFactory operationAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _OperationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
        _OperationAuthorizationFactory = operationAuthorizationFactory ?? throw new ArgumentNullException(nameof(operationAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public OperationError Execute(RemoveOperationPostData input)
    {
        _Logger.Information("RemoveOperation, ID = {1}", input.Id);

        var operation = _OperationFactory.GetByID(input.Id);
        if (operation == null) return new(ApiControlPlaneErrors.UnknownOperation, input.Id);

        _Logger.Warning("RemoveOperation: Disabling operation {0}->{1} before removing it!", operation.Service.Name, operation.Name);

        operation.Disable();

        Task.Run(() => DoRemoveOperation(operation));

        return null;
    }

    private void DoRemoveOperation(IOperation operation)
    {
        _Logger.Information("RemoveOperation: Deleting authorizations for Operation '{0}'", operation.Name);

        var operationAuthorizations = _OperationAuthorizationFactory.GetAllByOperation(operation);
        foreach (var operationAuthorization in operationAuthorizations)
            operationAuthorization.Delete();

        operation.Delete();
    }
}
