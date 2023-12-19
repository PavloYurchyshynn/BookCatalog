namespace BookCatalog.Application.Models.Order
{
    public class OrderModel
    {
        public Guid Id { get; set; }
        public Guid BasketId { get; set; }
        public string? DeliveryAddress { get; set; }
        public string? OrderStatus { get; set; }
        public string? Email { get; set; }
        public string? Name { get; set; }
        public int PhoneNumber { get; set; }
        public decimal? Price { get; set; }
    }
}
