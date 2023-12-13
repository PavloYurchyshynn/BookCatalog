using BookCatalog.Core.Common;

namespace BookCatalog.Core.Entities
{
    public class Comment : BaseEntity
    {
        public ICollection<Book>? Book { get; set; }
        public string? Text { get; set; }
    }
}
