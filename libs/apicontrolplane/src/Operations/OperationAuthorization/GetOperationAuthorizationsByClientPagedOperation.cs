namespace Roblox.ApiControlPlane;

using System;
using System.Linq;
using System.Collections.Generic;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting operation authorizations by client paged.
/// </summary>
public class GetOperationAuthorizationsByClientPagedOperation
    : IResultOperation<GetOperationAuthorizationsByClientPagedRequest, ICollection<OperationAuthorizationModel>>
{
    private readonly ILogger _Logger;
    private readonly IApiClientFactory _ApiClientFactory;
    private readonly IOperationAuthorizationFactory _OperationAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetOperationAuthorizationsByClientPagedOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <param name="operationAuthorizationFactory">The <see cref="IOperationAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// - <paramref name="operationAuthorizationFactory"/> cannot be null.
    /// </exception>
    public GetOperationAuthorizationsByClientPagedOperation(
        ILogger logger, 
        IApiClientFactory apiClientFactory, 
        IOperationAuthorizationFactory operationAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
        _OperationAuthorizationFactory = operationAuthorizationFactory ?? throw new ArgumentNullException(nameof(operationAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ICollection<OperationAuthorizationModel>, OperationError) Execute(GetOperationAuthorizationsByClientPagedRequest input)
    {
        if (input.StartRowIndex < 1) return (null, new("{0} must be greater than 0", nameof(input.StartRowIndex)));
        if (input.MaximumRows < 1) return (null, new("{0} must be greater than 0", nameof(input.MaximumRows)));

        _Logger.Information(
            "GetOperationAuthorizationsByClientPaged, Key = {0}, StartRowIndex = {1}, MaximumRows = {2}",
            input.Key,
            input.StartRowIndex, 
            input.MaximumRows
        );

        var apiClient = _ApiClientFactory.GetByKey(input.Key);
        if (apiClient == null) return (null, new(ApiControlPlaneErrors.UnknownClient, input.Key));

        return (_OperationAuthorizationFactory.GetAllByApiClient_Paged(
            apiClient,
            input.StartRowIndex, 
            input.MaximumRows)
            .Select(authorization => new OperationAuthorizationModel(authorization)).ToArray(), 
        null);
    }
}
