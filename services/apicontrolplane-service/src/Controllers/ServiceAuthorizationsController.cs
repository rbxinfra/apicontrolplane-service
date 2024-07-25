namespace Roblox.ApiControlPlane.Service.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using Roblox.Web.Framework.Services.Http;

using Models;

/// <summary>
/// Controller for accessing and modifying service authorizations.
/// </summary>
[Route("")]
[ApiController]
public class ServiceAuthorizationsController : Controller
{
    private readonly IOperationExecutor _OperationExecutor;
    private readonly IApiControlPlaneOperations _ApiControlPlaneOperations;

    /// <summary>
    /// Construct a new instance of <see cref="ServiceAuthorizationsController"/>
    /// </summary>
    /// <param name="operationExecutor">The <see cref="IOperationExecutor"/></param>
    /// <param name="apiControlPlaneOperations">The <see cref="IApiControlPlaneOperations"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="operationExecutor"/> cannot be null.
    /// - <paramref name="apiControlPlaneOperations"/> cannot be null.
    /// </exception>
    public ServiceAuthorizationsController(IOperationExecutor operationExecutor, IApiControlPlaneOperations apiControlPlaneOperations)
    {
        _OperationExecutor = operationExecutor ?? throw new ArgumentNullException(nameof(operationExecutor));
        _ApiControlPlaneOperations = apiControlPlaneOperations ?? throw new ArgumentNullException(nameof(apiControlPlaneOperations));
    }

    /// <summary>
    /// Creates, updates or removes a service authorization.
    /// </summary>
    /// <remarks>
    /// This is different from the other domains as this can do the following:<br />
    /// - Create new service authorizations<br />
    /// - Update authorization types on existing service authorizations<br />
    /// - Delete existing service authorizations (only if the AuthorizationType is None)<br />
    /// <br />
    /// <br />
    /// When deleteing, this will disable the service authorization (set as None) initially, 
    /// then delete each operation authorization associated with the service's operations and 
    /// service authorization API client only if <see cref="ISettings.RemoveServiceAuthorizationShouldDeleteOperationAuthorizations"/>
    /// is true, otherwise it will just remove the service authorization.<br /><br />
    /// </remarks>
    /// <param name="request">The <see cref="AddServiceAuthorizationPostData"/></param>
    /// <returns>The newly created operation</returns>
    /// <response code="400">
    /// ServiceName cannot be null or empty!<br />
    /// The service could not be found!<br />
    /// The client could not be found!
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(AddServiceAuthorization)}")]
    [ProducesResponseType(200, Type = typeof(ServiceAuthorizationPayload))]
    [ProducesResponseType(400)]
    public IActionResult AddServiceAuthorization([FromBody][ValidateNever] AddServiceAuthorizationPostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.AddServiceAuthorizationOperation, request);

    /// <summary>
    /// Gets a paged list of service authorizations by api client.
    /// </summary>
    /// <param name="request">The <see cref="GetServiceAuthorizationsByClientPagedRequest"/></param>
    /// <returns>The operations.</returns>
    /// <response code="400">
    /// StartRowIndex must be greater than 0!<br />
    /// MaximumRows must be greater than 0!<br />
    /// The client could not be found
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetServiceAuthorizationsByClientPaged)}")]
    [ProducesResponseType(200, Type = typeof(ServiceAuthorizationCollectionPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetServiceAuthorizationsByClientPaged(GetServiceAuthorizationsByClientPagedRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetServiceAuthorizationsByClientPagedOperation, request);

    /// <summary>
    /// Gets a paged list of service authorizations by service.
    /// </summary>
    /// <param name="request">The <see cref="GetServiceAuthorizationsByServicePagedRequest"/></param>
    /// <returns>The operations.</returns>
    /// <response code="400">
    /// ServiceName cannot be null or empty!<br />
    /// StartRowIndex must be greater than 0!<br />
    /// MaximumRows must be greater than 0!<br />
    /// The service could not be found
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetServiceAuthorizationsByServicePaged)}")]
    [ProducesResponseType(200, Type = typeof(ServiceAuthorizationCollectionPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetServiceAuthorizationsByServicePaged(GetServiceAuthorizationsByServicePagedRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetServiceAuthorizationsByServicePagedOperation, request);

    /// <summary>
    /// Gets total number of service authorizations by api client.
    /// </summary>
    /// <param name="request">The <see cref="GetTotalNumberOfServiceAuthorizationsByClientRequest"/></param>
    /// <returns>The operations.</returns>
    /// <response code="400">
    /// The client could not be found!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetTotalNumberOfServiceAuthorizationsByClient)}")]
    [ProducesResponseType(200, Type = typeof(IntPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetTotalNumberOfServiceAuthorizationsByClient(GetTotalNumberOfServiceAuthorizationsByClientRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetTotalNumberOfServiceAuthorizationsByClientOperation, request);

    /// <summary>
    /// Gets total number of service authorizations by service.
    /// </summary>
    /// <param name="request">The <see cref="GetTotalNumberOfServiceAuthorizationsByServiceRequest"/></param>
    /// <returns>The operations.</returns>
    /// <response code="400">
    /// ServiceName cannot be null or empty!<br />
    /// The service could not be found!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetTotalNumberOfServiceAuthorizationsByService)}")]
    [ProducesResponseType(200, Type = typeof(IntPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetTotalNumberOfServiceAuthorizationsByService(GetTotalNumberOfServiceAuthorizationsByServiceRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetTotalNumberOfServiceAuthorizationsByServiceOperation, request);
}
