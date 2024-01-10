using BookCatalog.Core.Common;
using BookCatalog.DataAccess.Persistence;
using BookCatalog.DataAccess.Repositories.Contracts;
using BookCatalog.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookCatalog.DataAccess.Repositories
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly BookCatalogContext _context;
        protected readonly DbSet<TEntity> _dbSet;

        /*public BaseRepository(IUnitOfWork<BookCatalogContext> unitOfWork) : this(unitOfWork.Context)
        { 
        }*/

        protected BaseRepository(BookCatalogContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task<TEntity> GetFirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var entity = await _dbSet.Where(predicate).FirstOrDefaultAsync();

            if (entity == null) throw new Exception("Book does not exist");

            return entity;
        }

        public IQueryable<TEntity> GetAll(params Expression<Func<TEntity, object>>[] includes)
        {
            var query = _context.Set<TEntity>().AsQueryable();
            return includes.Aggregate(query, (q, w) => q.Include(w));
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var addedEntity = (await _dbSet.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();

            return addedEntity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<TEntity> DeleteAsync(TEntity entity)
        {
            var removedEntity = _dbSet.Remove(entity).Entity;
            await _context.SaveChangesAsync();

            return removedEntity;
        }

        public IQueryable<TEntity> GetWhere(Expression<Func<TEntity, bool>> predicate)
        {
            return _context.Set<TEntity>().Where(predicate).AsQueryable();
        }
    }
}
