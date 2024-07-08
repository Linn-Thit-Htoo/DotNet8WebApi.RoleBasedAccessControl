using DotNet8WebApi.RoleBasedAccessControl.Models.Features.Auth;
using DotNet8WebApi.RoleBasedAccessControl.Models.Features;
using DotNet8WebApi.RoleBasedAccessControl.Repositories.Features.Auth;

namespace DotNet8WebApi.RoleBasedAccessControl.Api.Features.Auth
{
    public class BL_Auth
    {
        private readonly IAuthRepository _authRepository;

        public BL_Auth(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<Result<JwtResponseModel>> Login(LoginRequestModel requestModel)
        {
            var result = requestModel.IsValid();
            if (result.IsError)
            {
                goto result;
            }

            result = await _authRepository.Login(requestModel);

        result:
            return result;
        }
    }
}
