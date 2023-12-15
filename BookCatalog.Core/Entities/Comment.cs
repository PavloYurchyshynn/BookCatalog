using BookCatalog.Core.Common;

namespace BookCatalog.Core.Entities
{
    public class Comment : BaseEntity
    {
        public Guid BookId { get; set; }
        public Book? Book { get; set; }
        public string? Text { get; set; }
    }
}
