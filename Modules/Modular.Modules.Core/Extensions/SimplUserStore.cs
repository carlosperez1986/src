using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Modular.Modules.Core;
using Modular.Modules.Core.Data;
using Modular.Modules.Core.Models;

namespace Modular.Modules.Core.Extensions
{
    public class SimplUserStore : UserStore<User, Role, SimplDbContext, long, IdentityUserClaim<long>, UserRole,
        IdentityUserLogin<long>, IdentityUserToken<long>, IdentityRoleClaim<long>>
    {
        public SimplUserStore(SimplDbContext context, IdentityErrorDescriber describer) : base(context, describer)
        {
        }
    }
}
