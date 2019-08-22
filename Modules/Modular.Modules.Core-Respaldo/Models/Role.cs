using Microsoft.AspNetCore.Identity;
using Modular.Core.Models;
using Modular.Modules.Core.Models;
using System.Collections.Generic;

namespace Modular.Modules.Core.Models
{
    public class Role : IdentityRole<long>, IEntityWithTypedId<long>
    {
        public IList<UserRole> Users { get; set; } = new List<UserRole>();
    }
}
