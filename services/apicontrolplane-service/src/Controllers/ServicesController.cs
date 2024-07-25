namespace Roblox.ApiControlPlane.Service.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using Roblox.Web.Framework.Services.Http;

using Models;

/// <summary>
/// Controller for accessing and modifying services.
/// </summary>
[Route("")]
[ApiController]
public class ServicesController : Controller
{
    private readonly IOperationExecutor _OperationExecutor;
    private readonly IApiControlPlaneOperations _ApiControlPlaneOperations;

    /// <summary>
    /// Construct a new instance of <see cref="ServicesController"/>
    /// </summary>
    /// <param name="operationExecutor">The <see cref="IOperationExecutor"/></param>
    /// <param name="apiControlPlaneOperations">The <see cref="IApiControlPlaneOperations"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="operationExecutor"/> cannot be null.
    /// - <paramref name="apiControlPlaneOperations"/> cannot be null.
    /// </exception>
    public ServicesController(IOperationExecutor operationExecutor, IApiControlPlaneOperations apiControlPlaneOperations)
    {
        _OperationExecutor = operationExecutor ?? throw new ArgumentNullException(nameof(operationExecutor));
        _ApiControlPlaneOperations = apiControlPlaneOperations ?? throw new ArgumentNullException(nameof(apiControlPlaneOperations));
    }

    /// <summary>
    /// Creates a new Service.
    /// </summary>
    /// <param name="request">The <see cref="AddServicePostData"/></param>
    /// <returns>The newly created service</returns>
    /// <response code="400">
    /// Name cannot be null or empty!<br />
    /// The service already exists!
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(AddService)}")]
    [ProducesResponseType(200, Type = typeof(ServicePayload))]
    [ProducesResponseType(400)]
    public IActionResult AddService([FromBody][ValidateNever] AddServicePostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.AddServiceOperation, request);

    /// <summary>
    /// Gets a Service by ID.
    /// </summary>
    /// <param name="request">The <see cref="GetServiceByIDRequest"/></param>
    /// <returns>The service</returns>
    [HttpGet]
    [Route($"/v1/{nameof(GetServiceByID)}")]
    [ProducesResponseType(200, Type = typeof(ServicePayload))]
    public IActionResult GetServiceByID(GetServiceByIDRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetServiceByIDOperation, request);

    /// <summary>
    /// Gets a Service by Name.
    /// </summary>
    /// <param name="request">The <see cref="GetServiceByNameRequest"/></param>
    /// <returns>The service</returns>
    /// <response code="400">
    /// The note cannot be null or empty!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetServiceByName)}")]
    [ProducesResponseType(200, Type = typeof(ServicePayload))]
    [ProducesResponseType(400)]
    public IActionResult GetServiceByName(GetServiceByNameRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetServiceByNameOperation, request);

    /// <summary>
    /// Imports the configuration for a service.
    /// </summary>
    /// <remarks>
    /// This will do the following:<br />
    /// - Create the service if it does not exist<br />
    /// - Delete operations present in APCP DB but not in the config<br />
    /// - Create API clients if <see cref="ISettings.ImportClientAuthorizationsEnabled"/><br />
    /// - Create service/operation authorizations if <see cref="ISettings.ImportClientAuthorizationsEnabled"/><br />
    /// Please note, depending on how heavy the configuration is depends on how long this operation will take.
    /// <br />
    /// <br />
    /// </remarks>
    /// <param name="request">The <see cref="ImportServiceConfigurationPostData"/></param>
    /// <returns>The api client</returns>
    /// <response code="400">
    /// The service name cannot be null or empty!
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(ImportServiceConfiguration)}")]
    [ProducesResponseType(200, Type = typeof(ServiceRegistrationPayload))]
    [ProducesResponseType(400)]
    public IActionResult ImportServiceConfiguration([FromBody] ImportServiceConfigurationPostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.ImportServiceConfigurationOperation, request);

    /// <summary>
    /// Gets the registration of a service.
    /// </summary>
    /// <param name="request">The <see cref="GetServiceRegistrationRequest"/></param>
    /// <returns>The api client</returns>
    /// <response code="400">
    /// The service name cannot be null or empty!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetServiceRegistration)}")]
    [ProducesResponseType(200, Type = typeof(ServiceRegistrationPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetServiceRegistration(GetServiceRegistrationRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetServiceRegistrationOperation, request);

    /// <summary>
    /// Gets a paged list of Services.
    /// </summary>
    /// <param name="request">The <see cref="GetServicesPagedRequest"/></param>
    /// <returns>The services.</returns>
    /// <response code="400">
    /// StartRowIndex must be greater than 0!<br />
    /// MaximumRows must be greater than 0!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetServicesPaged)}")]
    [ProducesResponseType(200, Type = typeof(ServiceCollectionPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetServicesPaged(GetServicesPagedRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetServicesPagedOperation, request);

    /// <summary>
    /// Gets total number of Services.
    /// </summary>
    /// <returns>The services.</returns>
    /// <response code="400">
    /// StartRowIndex must be greater than 0!<br />
    /// MaximumRows must be greater than 0!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetTotalNumberOfServices)}")]
    [ProducesResponseType(200, Type = typeof(IntPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetTotalNumberOfServices()
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetTotalNumberOfServicesOperation);

    /// <summary>
    /// Removes a Service as well as all of its service and operation authorizations.
    /// </summary>
    /// <remarks>
    /// <span>This will disable the service initially, then delete each authorization, operation and the service in the background.<br /><br /></span>
    /// </remarks>
    /// <param name="request">The <see cref="RemoveServicePostData"/></param>
    /// <response code="400">
    /// The service could not be found
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(RemoveService)}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult RemoveService([FromBody][ValidateNever] RemoveServicePostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.RemoveServiceOperation, request);

    /// <summary>
    /// Updates a Service.
    /// </summary>
    /// <param name="request">The <see cref="UpdateServicePostData"/></param>
    /// <returns>The service</returns>
    /// <response code="400">
    /// The service could not be found<br />
    /// The new service cannot have the same API key as the service to be duplicated!
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(UpdateService)}")]
    [ProducesResponseType(200, Type = typeof(ServicePayload))]
    [ProducesResponseType(400)]
    public IActionResult UpdateService([FromBody][ValidateNever] UpdateServicePostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.UpdateServiceOperation, request);
}
