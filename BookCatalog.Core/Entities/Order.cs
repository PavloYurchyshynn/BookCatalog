using BookCatalog.Core.Common;

namespace BookCatalog.Core.Entities
{
    public class Order : BaseEntity
    {
        public Guid BasketId { get; set; }
        public Basket? Basket { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? OrderStatus { get; set; }
        public string? Email {  get; set; }
        public string? Name { get; set; }
        public int PhoneNumber { get; set; }
        public decimal? Price { get; set; }
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
