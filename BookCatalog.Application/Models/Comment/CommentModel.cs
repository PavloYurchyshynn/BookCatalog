namespace BookCatalog.Application.Models.Comment
{
    public class CommentModel
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public string? Text { get; set; }
    }
}
