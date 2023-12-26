using AutoMapper;
using BookCatalog.Application.Helpers;
using BookCatalog.Application.Models.Book;
using BookCatalog.Application.Services.Contracts;
using BookCatalog.Application.SyncDataServices.Http;
using BookCatalog.Core.Entities;
using BookCatalog.DataAccess.DapperRepositories.Contracts;
using BookCatalog.DataAccess.Repositories.Contracts;

namespace BookCatalog.Application.Services
{
    public class BookService : IBookService
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;
        private readonly IDapperBookRepository _dapperBookRepository;
        private readonly IBookCatalogServiceClient _bookCatalogServiceClient;

        public BookService(IMapper mapper, IBookRepository bookRepository, IDapperBookRepository dapperBookRepository, IBookCatalogServiceClient bookCatalogServiceClient)
        {
            _mapper = mapper;
            _bookRepository = bookRepository;
            _dapperBookRepository = dapperBookRepository;
            _bookCatalogServiceClient = bookCatalogServiceClient;
        }

        public IEnumerable<BookModel> GetAllBooks()
        {
            var books = _bookRepository.GetAll().AsParallel().ToList();
            if (books == null)
            {
                throw new Exception("Not found");
            }

            return _mapper.Map<IEnumerable<BookModel>>(books);
        }

        public async Task<BookModel> GetBookByIdAsync(Guid id)
        {
            var book = await _bookRepository.GetFirstAsync(b => b.id == id);

            if (book == null)
            {
                throw new Exception("Not found");
            }

            return _mapper.Map<BookModel>(book);
        }

        public async Task<IEnumerable<BookModel>> GetBooksByNameAsync(string name)
        {
            var books = await _dapperBookRepository.GetByNameAsync(name);
            if (books == null)
            {
                throw new Exception("Not found");
            }

            return _mapper.Map<IEnumerable<BookModel>>(books);
        }

        public async Task<BookModel> AddBookAsync(AddBookModel book)
        {
            var entity = _mapper.Map<Book>(book);
            if (entity == null)
            {
                throw new ArgumentNullException();
            }
            entity.Created = DateTime.Now;

            await _bookRepository.AddAsync(entity);

            await _bookCatalogServiceClient.SendBookToBookService(_mapper.Map<BookModel>(entity));

            return _mapper.Map<BookModel>(entity);
        }

        public async Task<BookModel> UpdateBookAsync(Guid id, UpdateBookModel model)
        {
            var book = await _bookRepository.GetFirstAsync(b => b.id == id);

            if (book == null)
            {
                throw new ArgumentNullException();
            }

            book.Author = model.Author;
            book.Price = model.Price;
            book.Description = model.Description;
            book.Name = model.Name;

            var updatedBook = await _bookRepository.UpdateAsync(book);

            return _mapper.Map<BookModel>(updatedBook);
        }

        public async Task<Response> DeleteBookAsync(Guid id)
        {
            var book = await _bookRepository.GetFirstAsync(b => b.id == id);
            if (book == null)
            {
                throw new ArgumentNullException();
            }

            await _bookRepository.DeleteAsync(book);

            return new Response { status = "Success", message = "Book deleted!" };
        }
    }
}
