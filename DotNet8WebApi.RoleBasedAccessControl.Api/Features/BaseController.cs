using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DotNet8WebApi.RoleBasedAccessControl.Api.Features;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected IActionResult Content(object obj)
    {
        return Content(JsonConvert.SerializeObject(obj), "application/json");
    }

    protected IActionResult InternalServerError(Exception ex)
    {
        return StatusCode(500, ex.Message);
    }
}
