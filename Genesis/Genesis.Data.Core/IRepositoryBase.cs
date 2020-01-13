using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Genesis.Data.Core
{
    public interface IRepositoryBase<T>
    {

        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetAsync(Guid id);
        Task<T> CreateAsync(T obj);
        Task DeleteAsync(Guid id);
        Task<T> UpdateAsync(Guid id, T obj);
    }
}
