namespace Roblox.ApiControlPlane;

using System;
using System.Linq;
using System.Collections.Generic;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for getting service authorizations by client paged.
/// </summary>
public class GetServiceAuthorizationsByClientPagedOperation
    : IResultOperation<GetServiceAuthorizationsByClientPagedRequest, ICollection<ServiceAuthorizationModel>>
{
    private readonly ILogger _Logger;
    private readonly IApiClientFactory _ApiClientFactory;
    private readonly IServiceAuthorizationFactory _ServiceAuthorizationFactory;

    /// <summary>
    /// Construct a new instance of <see cref="GetServiceAuthorizationsByClientPagedOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="apiClientFactory">The <see cref="IApiClientFactory"/></param>
    /// <param name="serviceAuthorizationFactory">The <see cref="IServiceAuthorizationFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="apiClientFactory"/> cannot be null.
    /// - <paramref name="serviceAuthorizationFactory"/> cannot be null.
    /// </exception>
    public GetServiceAuthorizationsByClientPagedOperation(
        ILogger logger, 
        IApiClientFactory apiClientFactory, 
        IServiceAuthorizationFactory serviceAuthorizationFactory
    )
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ApiClientFactory = apiClientFactory ?? throw new ArgumentNullException(nameof(apiClientFactory));
        _ServiceAuthorizationFactory = serviceAuthorizationFactory ?? throw new ArgumentNullException(nameof(serviceAuthorizationFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ICollection<ServiceAuthorizationModel>, OperationError) Execute(GetServiceAuthorizationsByClientPagedRequest input)
    {
        if (input.StartRowIndex < 1) return (null, new("{0} must be greater than 0", nameof(input.StartRowIndex)));
        if (input.MaximumRows < 1) return (null, new("{0} must be greater than 0", nameof(input.MaximumRows)));

        _Logger.Information(
            "GetServiceAuthorizationsByClientPaged, Key = {0}, StartRowIndex = {0}, MaximumRows = {1}",
            input.Key,
            input.StartRowIndex, 
            input.MaximumRows
        );

        var apiClient = _ApiClientFactory.GetByKey(input.Key);
        if (apiClient == null) return (null, new(ApiControlPlaneErrors.UnknownClient, input.Key));

        return (_ServiceAuthorizationFactory.GetAllByApiClient_Paged(
            apiClient,
            input.StartRowIndex, 
            input.MaximumRows)
            .Select(authorization => new ServiceAuthorizationModel(authorization)).ToArray(), 
        null);
    }
}
