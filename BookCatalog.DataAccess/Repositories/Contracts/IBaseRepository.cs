using BookCatalog.Core.Common;
using System.Linq.Expressions;

namespace BookCatalog.DataAccess.Repositories.Contracts
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task<TEntity> DeleteAsync(TEntity entity);
        IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate);
    }
}
