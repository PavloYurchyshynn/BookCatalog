using BookCatalog.Core.Entities;
using BookCatalog.DataAccess.Persistence;
using BookCatalog.DataAccess.Repositories.Contracts;

namespace BookCatalog.DataAccess.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(BookCatalogContext context) : base(context) { }
    }
}
