using System.Threading.Tasks;
using Modular.Modules.Core.Models;

namespace Modular.Modules.Core.Extensions
{
    public interface IWorkContext
    {
        Task<User> GetCurrentUser();
    }
}
