using System.Linq.Expressions;
using FitCoders.Domain;
using FitCoders.Domain.Entities.Base;

namespace FitCoders.Application.Repositories.Base
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T,bool>> predicate);
        Task<IReadOnlyList<T>> GetAsync(Expression<Func<T,bool>>? predicated,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            string? includeString,
            bool disableTracking = true);

        Task<IReadOnlyList<T>> GetAsync (Expression<Func<T,bool>>? predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            List<Expression<Func<T,object>>>? includes,
            bool disableTracking = true);

        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        void UpdatedAsync(T entity);
        void DeleteAsync(T entity);
    }
}