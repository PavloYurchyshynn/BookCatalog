using BookCatalog.Core.Entities;
using BookCatalog.DataAccess.Persistence;
using BookCatalog.DataAccess.Repositories.Contracts;
using static Dapper.SqlMapper;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BookCatalog.DataAccess.Repositories
{
    public class BasketRepository : BaseRepository<Basket>, IBasketRepository
    {
        protected readonly new DbSet<Basket> _dbSet;

        public BasketRepository(BookCatalogContext context) : base(context) 
        {
            _dbSet = context.Set<Basket>();
        }

        public new async Task<Basket> GetFirstAsync(Expression<Func<Basket, bool>> predicate)
        {
            var entity = await _dbSet.Where(predicate).FirstOrDefaultAsync();

            return entity;
        }
    }
}
