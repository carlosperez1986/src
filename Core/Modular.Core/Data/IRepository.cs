
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Modular.Core.Domain.Models;
using Modular.Core.Models;

namespace Modular.Core.Data
{
    public interface IRepository<T> : IRepository<T, long> where T : IEntity<long>
    {
    }

    public interface IRepository<T, in TId> where T : IEntity<TId>
    {
        IQueryable<T> Query();

        void Add(T entity);

        void Remove(T entity);

        void SaveChange();
    }
}