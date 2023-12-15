using BookCatalog.Application.Models.Comment;

namespace BookCatalog.Application.Services.Contracts
{
    public interface ICommentService
    {
        Task<CommentModel> AddCommentAsync(AddCommentModel comment);
    }
}
