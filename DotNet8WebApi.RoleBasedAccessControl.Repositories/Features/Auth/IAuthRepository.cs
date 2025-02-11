﻿namespace DotNet8WebApi.RoleBasedAccessControl.Repositories.Features.Auth;

public interface IAuthRepository
{
    Task<Result<JwtResponseModel>> Login(LoginRequestModel requestModel);
}
