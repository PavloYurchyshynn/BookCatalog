using System.ComponentModel.DataAnnotations;

namespace BookCatalog.Application.Models.Book
{
    public class AddBookModel
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Description { get; set; }
        [Required]
        public string? Author { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
