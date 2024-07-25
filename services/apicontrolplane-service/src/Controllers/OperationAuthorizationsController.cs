namespace Roblox.ApiControlPlane.Service.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using Roblox.Web.Framework.Services.Http;

using Models;

/// <summary>
/// Controller for accessing and modifying operation authorizations.
/// </summary>
[Route("")]
[ApiController]
public class OperationAuthorizationsController : Controller
{
    private readonly IOperationExecutor _OperationExecutor;
    private readonly IApiControlPlaneOperations _ApiControlPlaneOperations;

    /// <summary>
    /// Construct a new instance of <see cref="OperationAuthorizationsController"/>
    /// </summary>
    /// <param name="operationExecutor">The <see cref="IOperationExecutor"/></param>
    /// <param name="apiControlPlaneOperations">The <see cref="IApiControlPlaneOperations"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="operationExecutor"/> cannot be null.
    /// - <paramref name="apiControlPlaneOperations"/> cannot be null.
    /// </exception>
    public OperationAuthorizationsController(IOperationExecutor operationExecutor, IApiControlPlaneOperations apiControlPlaneOperations)
    {
        _OperationExecutor = operationExecutor ?? throw new ArgumentNullException(nameof(operationExecutor));
        _ApiControlPlaneOperations = apiControlPlaneOperations ?? throw new ArgumentNullException(nameof(apiControlPlaneOperations));
    }

    /// <summary>
    /// Creates, updates or removes a operation authorization.
    /// </summary>
    /// <remarks>
    /// This is different from the other domains as this can do the following:<br />
    /// - Create new operation authorizations<br />
    /// - Update authorization types on existing operation authorizations<br />
    /// - Delete existing operation authorizations (only if the AuthorizationType is None)<br />
    /// <br />
    /// <br />
    /// </remarks>
    /// <param name="request">The <see cref="AddOperationAuthorizationPostData"/></param>
    /// <returns>The newly created operation</returns>
    /// <response code="400">
    /// ServiceName cannot be null or empty!<br />
    /// OperationName cannot be null or empty!<br />
    /// The operation could not be found!<br />
    /// The client could not be found!
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(AddOperationAuthorization)}")]
    [ProducesResponseType(200, Type = typeof(OperationAuthorizationPayload))]
    [ProducesResponseType(400)]
    public IActionResult AddOperationAuthorization([FromBody][ValidateNever] AddOperationAuthorizationPostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.AddOperationAuthorizationOperation, request);

    /// <summary>
    /// Gets a paged list of operation authorizations by api client.
    /// </summary>
    /// <param name="request">The <see cref="GetOperationAuthorizationsByClientPagedRequest"/></param>
    /// <returns>The operations.</returns>
    /// <response code="400">
    /// StartRowIndex must be greater than 0!<br />
    /// MaximumRows must be greater than 0!<br />
    /// The client could not be found
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetOperationAuthorizationsByClientPaged)}")]
    [ProducesResponseType(200, Type = typeof(OperationAuthorizationCollectionPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetOperationAuthorizationsByClientPaged(GetOperationAuthorizationsByClientPagedRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetOperationAuthorizationsByClientPagedOperation, request);

    /// <summary>
    /// Gets a paged list of operation authorizations by operation.
    /// </summary>
    /// <param name="request">The <see cref="GetOperationAuthorizationsByOperationPagedRequest"/></param>
    /// <returns>The operations.</returns>
    /// <response code="400">
    /// ServiceName cannot be null or empty!<br />
    /// OperationName cannot be null or empty!<br />
    /// StartRowIndex must be greater than 0!<br />
    /// MaximumRows must be greater than 0!<br />
    /// The operation could not be found
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetOperationAuthorizationsByOperationPaged)}")]
    [ProducesResponseType(200, Type = typeof(OperationAuthorizationCollectionPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetOperationAuthorizationsByOperationPaged(GetOperationAuthorizationsByOperationPagedRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetOperationAuthorizationsByOperationPagedOperation, request);

    /// <summary>
    /// Gets total number of operation authorizations by api client.
    /// </summary>
    /// <param name="request">The <see cref="GetTotalNumberOfOperationAuthorizationsByClientRequest"/></param>
    /// <returns>The operations.</returns>
    /// <response code="400">
    /// The client could not be found!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetTotalNumberOfOperationAuthorizationsByClient)}")]
    [ProducesResponseType(200, Type = typeof(IntPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetTotalNumberOfOperationAuthorizationsByClient(GetTotalNumberOfOperationAuthorizationsByClientRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetTotalNumberOfOperationAuthorizationsByClientOperation, request);

    /// <summary>
    /// Gets total number of operation authorizations by operation.
    /// </summary>
    /// <param name="request">The <see cref="GetTotalNumberOfOperationAuthorizationsByOperationRequest"/></param>
    /// <returns>The operations.</returns>
    /// <response code="400">
    /// ServiceName cannot be null or empty!<br />
    /// OperationName cannot be null or empty!<br />
    /// The operation could not be found!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetTotalNumberOfOperationAuthorizationsByOperation)}")]
    [ProducesResponseType(200, Type = typeof(IntPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetTotalNumberOfOperationAuthorizationsByOperation(GetTotalNumberOfOperationAuthorizationsByOperationRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetTotalNumberOfOperationAuthorizationsByOperationOperation, request);
}
