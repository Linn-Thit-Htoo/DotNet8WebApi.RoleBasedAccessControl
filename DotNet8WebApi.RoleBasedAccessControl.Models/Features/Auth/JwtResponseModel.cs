namespace DotNet8WebApi.RoleBasedAccessControl.Models.Features.Auth;

public class JwtResponseModel
{
    public string UserName { get; set; }
    public string Email { get; set; }
    public string UserRole { get; set; }
}
