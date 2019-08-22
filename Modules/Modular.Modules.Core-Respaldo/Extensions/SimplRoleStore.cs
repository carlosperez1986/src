using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Modular.Modules.Core;
using Modular.Modules.Core.Data;
using Modular.Modules.Core.Models;

namespace Modular.Module.Core.Extensions
{
    public class SimplRoleStore : RoleStore<Role, SimplDbContext, long, UserRole, IdentityRoleClaim<long>>
    {
        public SimplRoleStore(SimplDbContext context) : base(context)
        {
        }
    }
}
