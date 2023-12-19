using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Application.Models.Order
{
    public class MakeOrderModel
    {
        public Guid BasketId { get; set; }
        public string? DeliveryAddress { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address.")]
        public string? Email { get; set; }
        public string? Name { get; set; }
        public int PhoneNumber { get; set; }
    }
}
