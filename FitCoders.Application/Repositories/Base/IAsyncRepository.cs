using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FitCoders.Application.Entities;

namespace FitCoders.Application.Repositories.Base
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T,bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T,bool>> predicated = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeString = null,
            bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAsync (Expression<Func<T,bool>> predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<Expression<Func<T,object>>> includes = null,
            bool disableTracking = true);

        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        void UpdatedAsync(T entity);
        void DeleteAsync(T entity);
    }
}