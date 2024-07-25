namespace Roblox.ApiControlPlane.Service.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

using Roblox.Web.Framework.Services.Http;

using Models;

/// <summary>
/// Controller for accessing and modifying operations.
/// </summary>
[Route("")]
[ApiController]
public class OperationsController : Controller
{
    private readonly IOperationExecutor _OperationExecutor;
    private readonly IApiControlPlaneOperations _ApiControlPlaneOperations;

    /// <summary>
    /// Construct a new instance of <see cref="OperationsController"/>
    /// </summary>
    /// <param name="operationExecutor">The <see cref="IOperationExecutor"/></param>
    /// <param name="apiControlPlaneOperations">The <see cref="IApiControlPlaneOperations"/></param>
    /// <exception cref="ArgumentNullException">
    /// - <paramref name="operationExecutor"/> cannot be null.
    /// - <paramref name="apiControlPlaneOperations"/> cannot be null.
    /// </exception>
    public OperationsController(IOperationExecutor operationExecutor, IApiControlPlaneOperations apiControlPlaneOperations)
    {
        _OperationExecutor = operationExecutor ?? throw new ArgumentNullException(nameof(operationExecutor));
        _ApiControlPlaneOperations = apiControlPlaneOperations ?? throw new ArgumentNullException(nameof(apiControlPlaneOperations));
    }

    /// <summary>
    /// Creates a new Operation.
    /// </summary>
    /// <param name="request">The <see cref="AddOperationPostData"/></param>
    /// <returns>The newly created operation</returns>
    /// <response code="400">
    /// Name cannot be null or empty!<br />
    /// ServiceName cannot be null or empty!<br />
    /// The service could not be found!<br />
    /// The operation already exists!
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(AddOperation)}")]
    [ProducesResponseType(200, Type = typeof(OperationPayload))]
    [ProducesResponseType(400)]
    public IActionResult AddOperation([FromBody][ValidateNever] AddOperationPostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.AddOperationOperation, request);

    /// <summary>
    /// Gets a Operation by ID.
    /// </summary>
    /// <param name="request">The <see cref="GetOperationByIDRequest"/></param>
    /// <returns>The operation</returns>
    [HttpGet]
    [Route($"/v1/{nameof(GetOperationByID)}")]
    [ProducesResponseType(200, Type = typeof(OperationPayload))]
    public IActionResult GetOperationByID(GetOperationByIDRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetOperationByIDOperation, request);

    /// <summary>
    /// Gets a Operation by Service.
    /// </summary>
    /// <param name="request">The <see cref="GetOperationByServiceRequest"/></param>
    /// <returns>The operation</returns>
    /// <response code="400">
    /// The name cannot be null or empty!<br />
    /// The ServiceName cannot be null or empty!<br />
    /// The service could not be found!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetOperationByService)}")]
    [ProducesResponseType(200, Type = typeof(OperationPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetOperationByService(GetOperationByServiceRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetOperationByServiceOperation, request);

    /// <summary>
    /// Gets a paged list of Operations.
    /// </summary>
    /// <param name="request">The <see cref="GetOperationsByServicePagedRequest"/></param>
    /// <returns>The operations.</returns>
    /// <response code="400">
    /// ServiceName cannot be null or empty!<br />
    /// StartRowIndex must be greater than 0!<br />
    /// MaximumRows must be greater than 0!<br />
    /// The service could not be found
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetOperationsByServicePaged)}")]
    [ProducesResponseType(200, Type = typeof(OperationCollectionPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetOperationsByServicePaged(GetOperationsByServicePagedRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetOperationsByServicePagedOperation, request);

    /// <summary>
    /// Gets total number of Operations.
    /// </summary>
    /// <param name="request">The <see cref="GetTotalNumberOfOperationsByServiceRequest"/></param>
    /// <returns>The operations.</returns>
    /// <response code="400">
    /// ServiceName cannot be null or empty!<br />
    /// The service could not be found!
    /// </response>
    [HttpGet]
    [Route($"/v1/{nameof(GetTotalNumberOfOperationsByService)}")]
    [ProducesResponseType(200, Type = typeof(IntPayload))]
    [ProducesResponseType(400)]
    public IActionResult GetTotalNumberOfOperationsByService(GetTotalNumberOfOperationsByServiceRequest request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.GetTotalNumberOfOperationsByServiceOperation, request);

    /// <summary>
    /// Removes a Operation as well as all of its operation and operation authorizations.
    /// </summary>
    /// <remarks>
    /// <span>This will disable the operation initially, then delete each authorization and the operation in the background.<br /><br /></span>
    /// </remarks>
    /// <param name="request">The <see cref="RemoveOperationPostData"/></param>
    /// <response code="400">
    /// The operation could not be found
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(RemoveOperation)}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult RemoveOperation([FromBody][ValidateNever] RemoveOperationPostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.RemoveOperationOperation, request);

    /// <summary>
    /// Updates a Operation.
    /// </summary>
    /// <param name="request">The <see cref="UpdateOperationPostData"/></param>
    /// <returns>The operation</returns>
    /// <response code="400">
    /// The operation could not be found<br />
    /// The new operation cannot have the same API key as the operation to be duplicated!
    /// </response>
    [HttpPost]
    [Route($"/v1/{nameof(UpdateOperation)}")]
    [ProducesResponseType(200, Type = typeof(OperationPayload))]
    [ProducesResponseType(400)]
    public IActionResult UpdateOperation([FromBody][ValidateNever] UpdateOperationPostData request)
        => _OperationExecutor.Execute(_ApiControlPlaneOperations.UpdateOperationOperation, request);
}
