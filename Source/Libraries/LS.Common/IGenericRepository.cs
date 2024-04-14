using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LS.Common
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> GetQuery();
        IEnumerable<T> GetAll();
        Task<List<T>> GetAllAsync();
        T GetById(Guid id);
        T GetByIdWithIncludes(Guid id);
        Task<T> GetByIdAsync(Guid id);
        Task<T> GetByIdWithIncludesAsync(Guid id);
        bool Remove(Guid id);
        void Add(in T sender);
        void Update(in T sender);
        int Save();
        Task<int> SaveAsync();
        T Select(Expression<Func<T, bool>> predicate);
        Task<T> SelectAsync(Expression<Func<T, bool>> predicate);
    }
}