namespace Roblox.ApiControlPlane.Service.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using Roblox.Web.Framework.Services.Http;

using Models;

/// <summary>
/// Controller for accessing and modifying Api Clients.
/// </summary>
[Route("")]
[ApiController]
public class ApiClientsController : Controller
{
    private readonly IOperationExecutor _OperationExecutor;
    private readonly IApiControlPlaneOperations _ApiControlPlaneOperations;

    /// <summary>
    /// Construct a new instance of <see cref="ApiClientsController"/>
    /// </summary>
    /// <param name="operationExecutor">The <see cref="IOperationExecutor"/></param>
    /// <param name="apiControlPlaneOperations">The <see cref="IApiControlPlaneOperations"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="operationExecutor"/> cannot be null.
    /// - <paramref name="apiControlPlaneOperations"/> cannot be null.
    /// </exception>
    public ApiClientsController(IOperationExecutor operationExecutor, IApiControlPlaneOperations apiControlPlaneOperations)
    {
        _OperationExecutor = operationExecutor ?? throw new ArgumentNullException(nameof(operationExecutor));
        _ApiControlPlaneOperations = apiControlPlaneOperations ?? throw new ArgumentNullException(nameof(apiControlPlaneOperations));
    }

    /// <summary>
    /// Creates a new API client.
    /// </summary>
    /// <param name="request">The <see cref="AddClientPostData"/></param>
    /// <returns>The newly created api client</returns>
    /// <response code="400">
    /// Note cannot be null or empty!<br />
    /// The client already exists!
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(AddClient)}")]
    [ProducesResponseType(200, Type = typeof(ClientPayload))]
    [ProducesResponseType(400)]
    public IActionResult AddClient([FromBody][ValidateNever] AddClientPostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.AddClientOperation, request);

    /// <summary>
    /// Duplicates an API client with all of its service and operation authorizations.
    /// </summary>
    /// <remarks>
    /// <span>This will create the authorizations in the background just in case there are many<br /><br /></span>
    /// </remarks>
    /// <param name="request">The <see cref="DuplicateClientPostData"/></param>
    /// <returns>The newly created api client</returns>
    /// <response code="400">
    /// The client could not be found<br />
    /// The new client cannot have the same API key as the client to be duplicated!
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(DuplicateClient)}")]
    [ProducesResponseType(200, Type = typeof(ClientPayload))]
    [ProducesResponseType(400)]
    public IActionResult DuplicateClient([FromBody][ValidateNever] DuplicateClientPostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.DuplicateClientOperation, request);

    /// <summary>
    /// Gets an API client by ID.
    /// </summary>
    /// <param name="request">The <see cref="GetClientByIDRequest"/></param>
    /// <returns>The api client</returns>
    [HttpGet]
    [Route($"/v1/{nameof(GetClientByID)}")]
    [ProducesResponseType(200, Type = typeof(ClientPayload))]
    public IActionResult GetClientByID(GetClientByIDRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetClientByIDOperation, request);

    /// <summary>
    /// Gets an API client by Key.
    /// </summary>
    /// <param name="request">The <see cref="GetClientByKeyRequest"/></param>
    /// <returns>The api client</returns>
    [HttpGet]
    [Route($"/v1/{nameof(GetClientByKey)}")]
    [ProducesResponseType(200, Type = typeof(ClientPayload))]
    public IActionResult GetClientByKey(GetClientByKeyRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetClientByKeyOperation, request);

    /// <summary>
    /// Gets an API client by ID.
    /// </summary>
    /// <param name="request">The <see cref="GetClientByNoteRequest"/></param>
    /// <returns>The api client</returns>
    /// <response code="400">
    /// The note cannot be null or empty!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetClientByNote)}")]
    [ProducesResponseType(200, Type = typeof(ClientPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetClientByNote(GetClientByNoteRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetClientByNoteOperation, request);

    /// <summary>
    /// Gets a paged list of API clients.
    /// </summary>
    /// <param name="request">The <see cref="GetClientsPagedRequest"/></param>
    /// <returns>The api clients.</returns>
    /// <response code="400">
    /// StartRowIndex must be greater than 0!<br />
    /// MaximumRows must be greater than 0!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetClientsPaged)}")]
    [ProducesResponseType(200, Type = typeof(ClientCollectionPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetClientsPaged(GetClientsPagedRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetClientsPagedOperation, request);

    /// <summary>
    /// Gets total number of API clients.
    /// </summary>
    /// <returns>The api clients.</returns>
    /// <response code="400">
    /// StartRowIndex must be greater than 0!<br />
    /// MaximumRows must be greater than 0!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetTotalNumberOfClients)}")]
    [ProducesResponseType(200, Type = typeof(IntPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetTotalNumberOfClients()
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetTotalNumberOfClientsOperation);

    /// <summary>
    /// Removes an API client as well as all of its service and operation authorizations.
    /// </summary>
    /// <remarks>
    /// <span>This will set the client invalid initially, then delete each authorization and the api client in the background.<br /><br /></span>
    /// </remarks>
    /// <param name="request">The <see cref="RemoveClientPostData"/></param>
    /// <response code="400">
    /// The client could not be found
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(RemoveClient)}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult RemoveClient([FromBody][ValidateNever] RemoveClientPostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.RemoveClientOperation, request);

    /// <summary>
    /// Updates an API client.
    /// </summary>
    /// <param name="request">The <see cref="UpdateClientPostData"/></param>
    /// <returns>The api client</returns>
    /// <response code="400">
    /// The client could not be found<br />
    /// The new client cannot have the same API key as the client to be duplicated!
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(UpdateClient)}")]
    [ProducesResponseType(200, Type = typeof(ClientPayload))]
    [ProducesResponseType(400)]
    public IActionResult UpdateClient([FromBody][ValidateNever] UpdateClientPostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.UpdateClientOperation, request);
}
