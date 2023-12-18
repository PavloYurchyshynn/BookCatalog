using BookCatalog.Core.Common;
using System.Runtime.CompilerServices;

namespace BookCatalog.Core.Entities
{
    public class Book : BaseEntity
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Author { get; set; }
        public decimal Price { get; set; }
        public DateTime? Created { get; set; }
        public ICollection<Comment>? Comment { get; set; }
        public ICollection<BasketItem>? BasketItems { get; set; }
    }
}
