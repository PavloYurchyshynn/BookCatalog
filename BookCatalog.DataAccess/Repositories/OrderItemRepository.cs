using BookCatalog.Core.Entities;
using BookCatalog.DataAccess.Persistence;
using BookCatalog.DataAccess.Repositories.Contracts;

namespace BookCatalog.DataAccess.Repositories
{
    public class OrderItemRepository : BaseRepository<OrderItem>, IOrderItemRepository
    {
        public OrderItemRepository(BookCatalogContext context) : base(context) { }
    }
}
