using DotNet8WebApi.RoleBasedAccessControl.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8WebApi.RoleBasedAccessControl.Models.Features.Auth
{
    public class LoginRequestModel
    {
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public Result<JwtResponseModel> IsValid()
        {
            Result<JwtResponseModel> result;
            if (Email.IsNullOrEmpty())
            {
                result = Result<JwtResponseModel>.FailureResult("Email cannot be empty.");
                goto result;
            }

            if (Password.IsNullOrEmpty())
            {
                result = Result<JwtResponseModel>.FailureResult("Password cannot be empty.");
                goto result;
            }

            result = Result<JwtResponseModel>.SuccessResult();

        result:
            return result;
        }
    }
}
