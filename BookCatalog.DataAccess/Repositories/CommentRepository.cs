using BookCatalog.Core.Entities;
using BookCatalog.DataAccess.Persistence;
using BookCatalog.DataAccess.Repositories.Contracts;

namespace BookCatalog.DataAccess.Repositories
{
    public class CommentRepository : BaseRepository<Comment>, ICommentRepository
    {
        public CommentRepository(BookCatalogContext context) : base(context) { }
    }
}
