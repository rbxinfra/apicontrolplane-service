namespace Roblox.ApiControlPlane.Service.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using ApplicationContext;

/// <summary>
/// Health check controller.
/// </summary>
[Route("")]
[ApiController]
[AllowAnonymous]
[ApiExplorerSettings(IgnoreApi = true)]
public class HealthCheckController : Controller
{
    private readonly IApplicationContext _ApplicationContext;

    /// <summary>
    /// Constructs a new instance of <see cref="HealthCheckController"/>
    /// </summary>
    /// <param name="applicationContext">The <see cref="IApplicationContext"/></param>
    /// <exception cref="ArgumentNullException"><paramref name="applicationContext"/> cannot be null.</exception>
    public HealthCheckController(IApplicationContext applicationContext)
    {
        _ApplicationContext = applicationContext ?? throw new ArgumentNullException(nameof(applicationContext));
    }

    /// <summary>
    /// Check health endpoint.
    /// </summary>
    /// <returns>The health check result.</returns>
    [HttpGet]
    [Route("")]
    [Route("/checkhealth")]
    public ActionResult CheckHealth()
        => Json(new { name = _ApplicationContext.Name, status = "OK" });
}
