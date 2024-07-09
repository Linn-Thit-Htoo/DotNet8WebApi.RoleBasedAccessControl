using DotNet8WebApi.RoleBasedAccessControl.Models.Features;
using DotNet8WebApi.RoleBasedAccessControl.Models.Features.Auth;
using DotNet8WebApi.RoleBasedAccessControl.Shared.Services.AuthService;

namespace DotNet8WebApi.RoleBasedAccessControl.Repositories.Features.Auth;

public interface IAuthRepository
{
    Task<Result<JwtResponseModel>> Login(LoginRequestModel requestModel);
}
