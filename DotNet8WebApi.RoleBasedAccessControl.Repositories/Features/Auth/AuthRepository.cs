namespace DotNet8WebApi.RoleBasedAccessControl.Repositories.Features.Auth;

public class AuthRepository : IAuthRepository
{
    private readonly AppDbContext _appDbContext;
    private readonly AesService _aesService;

    public AuthRepository(AppDbContext appDbContext, AesService esService)
    {
        _appDbContext = appDbContext;
        _aesService = esService;
    }

    public async Task<Result<JwtResponseModel>> Login(LoginRequestModel requestModel)
    {
        Result<JwtResponseModel> responseModel;
        try
        {
            var user = await _appDbContext.TblUsers.FirstOrDefaultAsync(x =>
                x.Email == requestModel.Email && x.Password == requestModel.Password && x.IsActive
            );
            if (user is null)
            {
                responseModel = Result<JwtResponseModel>.FailureResult(
                    MessageResource.NotFound,
                    EnumStatusCode.NotFound
                );
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
            UserName = _aesService.EncryptString(user.UserName),
            Email = _aesService.EncryptString(user.Email),
            UserRole = _aesService.EncryptString(user.UserRole)
        };
    }
}
