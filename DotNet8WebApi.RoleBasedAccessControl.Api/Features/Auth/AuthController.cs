using DotNet8WebApi.RoleBasedAccessControl.Models.Features.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8WebApi.RoleBasedAccessControl.Api.Features.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly BL_Auth _bLAuth;

        public AuthController(BL_Auth bLAuth)
        {
            _bLAuth = bLAuth;
        }

        [HttpPost]
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
}
