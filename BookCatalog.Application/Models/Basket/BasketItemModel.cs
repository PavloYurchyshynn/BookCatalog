namespace BookCatalog.Application.Models.Basket
{
    public class BasketItemModel
    {
        public Guid BookId { get; set; }
        public int Quantity { get; set; }
    }
}
