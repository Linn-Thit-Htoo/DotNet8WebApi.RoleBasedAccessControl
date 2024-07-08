using DotNet8WebApi.RoleBasedAccessControl.DbService.AppDbContexts;
using DotNet8WebApi.RoleBasedAccessControl.Models.Enums;
using DotNet8WebApi.RoleBasedAccessControl.Models.Features;
using DotNet8WebApi.RoleBasedAccessControl.Models.Features.Auth;
using DotNet8WebApi.RoleBasedAccessControl.Models.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8WebApi.RoleBasedAccessControl.Repositories.Features.Auth
{
    public class AuthRepository : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;

        public AuthRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Result<JwtResponseModel>> Login(LoginRequestModel requestModel)
        {
            Result<JwtResponseModel> responseModel;
            try
            {
                var user = await _appDbContext.TblUsers.FirstOrDefaultAsync(x => x.Email == requestModel.Email
                && x.Password == requestModel.Password && x.IsActive);
                if (user is null)
                {
                    responseModel = Result<JwtResponseModel>.FailureResult(MessageResource.NotFound, EnumStatusCode.NotFound);
                    goto result;
                }

                var model = GetJwtResponseModel(user);
                responseModel = Result<JwtResponseModel>.SuccessResult(model);
            }
            catch (Exception ex)
            {
                responseModel = Result<JwtResponseModel>.FailureResult(ex);
            }

        result:
            return responseModel;
        }

        private JwtResponseModel GetJwtResponseModel(TblUser user)
        {
            return new JwtResponseModel
            {
                UserName = user.UserName,
                Email = user.Email,
                UserRole = user.UserRole
            };
        }
    }
}
