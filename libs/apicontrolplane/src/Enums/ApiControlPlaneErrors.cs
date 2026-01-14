namespace Roblox.ApiControlPlane;

using System.Net;
using System.ComponentModel;

using Roblox.Web.Framework.Services.Http;

/// <summary>
/// Error codes from the API.
/// </summary>
internal enum ApiControlPlaneErrors
{
    /// <summary>
    /// The client could not be found.
    /// </summary>
    [HttpStatusCode(HttpStatusCode.NotFound)]
    [Description("The client '{0}' could not be found")]
    UnknownClient,

    /// <summary>
    /// The client already exists.
    /// </summary>
    [HttpStatusCode(HttpStatusCode.Conflict)]
    [Description("The client '{0}' already exists as '{1}'")]
    ClientAlreadyExists,

    /// <summary>
    /// The new client cannot have the same API key as the client to be duplicated
    /// </summary>
    [HttpStatusCode(HttpStatusCode.Conflict)]
    [Description("The new client cannot have the same API key as the client to be duplicated!")]
    ClientKeyConflict,

    /// <summary>
    /// The service already exists.
    /// </summary>
    [HttpStatusCode(HttpStatusCode.Conflict)]
    [Description("The service '{0}' already exists")]
    ServiceAlreadyExists,

    /// <summary>
    /// Unknown service
    /// </summary>
    [HttpStatusCode(HttpStatusCode.NotFound)]
    [Description("The service '{0}' could not be found")]
    UnknownService,

    /// <summary>
    /// The operation already exists.
    /// </summary>
    [HttpStatusCode(HttpStatusCode.Conflict)]
    [Description("The operation '{0}->{1}' already exists")]
    OperationAlreadyExists,

    /// <summary>
    /// Unknown operation.
    /// </summary>
    [HttpStatusCode(HttpStatusCode.Conflict)]
    [Description("The service '{0}' could not be found")]
    UnknownOperation
}
