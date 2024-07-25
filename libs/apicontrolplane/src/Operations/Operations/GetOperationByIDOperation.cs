namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting an operation.
/// </summary>
public class GetOperationByIDOperation : IResultOperation<GetOperationByIDRequest, OperationModel>
{
    private readonly ILogger _Logger;
    private readonly IOperationFactory _OperationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetServiceByIDOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="operationFactory">The <see cref="IOperationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="operationFactory"/> cannot be null.
    /// </exception>
    public GetOperationByIDOperation(ILogger logger, IOperationFactory operationFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _OperationFactory = operationFactory ?? throw new ArgumentNullException(nameof(operationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (OperationModel, OperationError) Execute(GetOperationByIDRequest input)
    {
        _Logger.Information("GetOperationByID, ID = {0}", input.Id);

        var operation = _OperationFactory.GetByID(input.Id);

        return (operation != null ? new(operation) : null, null);
    }
}
