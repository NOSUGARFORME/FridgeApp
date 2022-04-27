using Microsoft.AspNetCore.Mvc;

namespace FridgeApp.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected ActionResult<TResult> OkOrNotFound<TResult>(TResult result)
            => result is null ? NotFound() : Ok(result);
    }
}