namespace DotNet8WebApi.RoleBasedAccessControl.DbService.AppDbContexts;

public partial class TblUser
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string UserRole { get; set; } = null!;

    public string Password { get; set; } = null!;

    public bool IsActive { get; set; }
}
