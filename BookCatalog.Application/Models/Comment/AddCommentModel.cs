namespace BookCatalog.Application.Models.Comment
{
    public class AddCommentModel
    {
        public Guid BookId { get; set; }
        public string? Text { get; set; }
    }
}
