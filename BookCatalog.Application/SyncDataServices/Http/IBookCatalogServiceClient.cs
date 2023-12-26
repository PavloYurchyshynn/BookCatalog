using BookCatalog.Application.Models.Book;

namespace BookCatalog.Application.SyncDataServices.Http
{
    public interface IBookCatalogServiceClient
    {
        Task SendBookToBookService(BookModel model);
    }
}
