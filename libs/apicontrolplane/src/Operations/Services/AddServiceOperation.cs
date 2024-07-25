namespace Roblox.ApiControlPlane;

using System;

using EventLog;
using Operations;
using Api.ControlPlane;

using Models;

/// <summary>
/// Operation for creating a new service.
/// </summary>
public class AddServiceOperation : IResultOperation<AddServicePostData, ServiceModel>
{
    private readonly ILogger _Logger;
    private readonly IServiceFactory _ServiceFactory;

    /// <summary>
    /// Construct a new instance of <see cref="AddServiceOperation"/>
    /// </summary>
    /// <param name="logger">The <see cref="ILogger"/></param>
    /// <param name="serviceFactory">The <see cref="IServiceFactory"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="logger"/> cannot be null.
    /// - <paramref name="serviceFactory"/> cannot be null.
    /// </exception>
    public AddServiceOperation(ILogger logger, IServiceFactory serviceFactory)
    {
        _Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        _ServiceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
    }

    /// <inheritdoc cref="IOperation{TInput}.Execute(TInput)"/>
    public (ServiceModel, OperationError) Execute(AddServicePostData input)
    {
        if (string.IsNullOrEmpty(input.Name)) return (null, new("{0} cannot be null or empty", nameof(input.Name)));

        _Logger.Information("AddService, Name = {0}, IsEnabled = {1}", input.Name, input.IsEnabled);

        if (_ServiceFactory.GetByName(input.Name) != null) return (null, new(ApiControlPlaneErrors.ServiceAlreadyExists, input.Name));

        return (new(_ServiceFactory.CreateNew(input.Name, input.IsEnabled)), null);
    }
}
