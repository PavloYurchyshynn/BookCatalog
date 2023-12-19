using BookCatalog.Core.Common;

namespace BookCatalog.Core.Entities
{
    public class OrderItem : BaseEntity
    {
        public Guid OrderId { get; set; }
        public Guid BookId { get; set; }
        public Order? Order { get; set; }
        public int Quantity { get; set; }
    }
}
