using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Services
{
    public interface IRepository<T> where T:class
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        void Add(T value);
        void AddRange(IEnumerable<T> entitiesList);
        void Remove(T entity);
        void Remove(Guid entityId);
        void RemoveRange(IEnumerable<T> entitiesLisst);
        Task SaveChangesAsync();
    }
}
