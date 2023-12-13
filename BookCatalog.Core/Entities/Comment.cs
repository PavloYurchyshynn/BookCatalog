using BookCatalog.Core.Common;

namespace BookCatalog.Core.Entities
{
    public class Comment : BaseEntity
    {
        public Book? Book { get; set; }
        public string? Text { get; set; }
    }
}
