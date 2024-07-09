using DotNet8WebApi.RoleBasedAccessControl.Models.Enums;
using DotNet8WebApi.RoleBasedAccessControl.Models.Features.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.RoleBasedAccessControl.Api.Features.Auth;

[Route("api/account")]
[ApiController]
public class AuthController : BaseController
{
    private readonly BL_Auth _bLAuth;

    public AuthController(BL_Auth bLAuth)
    {
        _bLAuth = bLAuth;
    }

    [HttpGet]
    [Authorize(Policy = "AdminOnly")]
    public IActionResult Test()
    {
        return Ok("Hello!");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestModel requestModel)
    {
        try
        {
            var result = await _bLAuth.Login(requestModel);
            return Content(result);
        }
        catch (Exception ex)
        {
            return InternalServerError(ex);
        }
    }
}
