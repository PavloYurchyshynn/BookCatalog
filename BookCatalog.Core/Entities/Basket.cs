using BookCatalog.Core.Common;

namespace BookCatalog.Core.Entities
{
    public class Basket : BaseEntity
    {
        public decimal TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
        public ICollection<BasketItem>? Items { get; set; }
    }
}
