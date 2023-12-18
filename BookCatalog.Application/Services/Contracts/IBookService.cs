using BookCatalog.Application.Helpers;
using BookCatalog.Application.Models.Book;

namespace BookCatalog.Application.Services.Contracts
{
    public interface IBookService
    {
        IEnumerable<BookModel> GetAllBooks();
        Task<BookModel> GetBookByIdAsync(Guid id);
        Task<IEnumerable<BookModel>> GetBooksByNameAsync(string name);
        Task<BookModel> AddBookAsync(AddBookModel book);
        Task<BookModel> UpdateBookAsync(Guid id, UpdateBookModel book);
        Task<Response> DeleteBookAsync(Guid id);
    }
}
