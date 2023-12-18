namespace BookCatalog.Application.Models.Basket
{
    public class BasketModel
    {
        public Guid Id { get; set; }
        public decimal TotalAmount { get; set; }
        public int TotalQuantity { get; set; }
    }
}
