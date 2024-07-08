using DotNet8WebApi.RoleBasedAccessControl.Models.Features;
using DotNet8WebApi.RoleBasedAccessControl.Models.Features.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8WebApi.RoleBasedAccessControl.Repositories.Features.Auth
{
    public interface IAuthRepository
    {
        Task<Result<JwtResponseModel>> Login(LoginRequestModel requestModel);
    }
}
