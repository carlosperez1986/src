using Modular.Core.Models;

namespace Modular.Core.Data
{

    public interface IRepository<T> : IRepositoryWithTypedId<T, long> where T : IEntityWithTypedId<long>
    {
    }
}