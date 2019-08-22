using System.Linq;
using Modular.Modules.Core.Models;

namespace Modular.Modules.Core.Services
{
    public interface IWidgetInstanceService
    {
        IQueryable<WidgetInstance> GetPublished();
    }
}
