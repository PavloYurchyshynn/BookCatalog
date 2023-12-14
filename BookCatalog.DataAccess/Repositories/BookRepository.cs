using BookCatalog.Core.Entities;
using BookCatalog.DataAccess.Persistence;
using BookCatalog.DataAccess.Repositories.Contracts;

namespace BookCatalog.DataAccess.Repositories
{
    public class BookRepository : BaseRepository<Book>, IBookRepository
    {
        public BookRepository(BookCatalogContext context) : base(context) { }
    }
}
