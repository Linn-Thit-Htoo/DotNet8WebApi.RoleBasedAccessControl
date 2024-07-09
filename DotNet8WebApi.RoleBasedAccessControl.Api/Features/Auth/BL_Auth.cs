using DotNet8WebApi.RoleBasedAccessControl.Models.Features;
using DotNet8WebApi.RoleBasedAccessControl.Models.Features.Auth;
using DotNet8WebApi.RoleBasedAccessControl.Repositories.Features.Auth;
using DotNet8WebApi.RoleBasedAccessControl.Shared.Services.AuthService;

namespace DotNet8WebApi.RoleBasedAccessControl.Api.Features.Auth;

public class BL_Auth
{
    private readonly IAuthRepository _authRepository;
    private readonly JWTAuth _jwtAuth;

    public BL_Auth(IAuthRepository authRepository, JWTAuth jwtAuth)
    {
        _authRepository = authRepository;
        _jwtAuth = jwtAuth;
    }

    public async Task<Result<JwtResponseModel>> Login(LoginRequestModel requestModel)
    {
        var result = requestModel.IsValid();
        if (result.IsError)
        {
            goto result;
        }

        result = await _authRepository.Login(requestModel);
        if (result.IsSuccess)
        {
            result.Token = _jwtAuth.GetJWTToken(result.Data);
        }

    result:
        return result;
    }
}
