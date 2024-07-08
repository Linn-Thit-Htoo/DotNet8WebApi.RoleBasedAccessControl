using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNet8WebApi.RoleBasedAccessControl.Models.Features.Auth
{
    public class UserModel
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string UserRole { get; set; } = null!;

        public string Password { get; set; } = null!;

        public bool IsActive { get; set; }
    }
}
