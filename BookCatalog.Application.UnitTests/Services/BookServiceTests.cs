using AutoMapper;
using BookCatalog.Application.MappingProfiles;
using BookCatalog.Application.Models.Book;
using BookCatalog.Application.Services;
using BookCatalog.Application.Services.Contracts;
using BookCatalog.Application.SyncDataServices.Http;
using BookCatalog.Core.Entities;
using BookCatalog.DataAccess.DapperRepositories.Contracts;
using BookCatalog.DataAccess.Repositories.Contracts;
using Moq;
using System.Linq.Expressions;

namespace BookCatalog.Application.UnitTests.Services
{
    public class BookServiceTests
    {
        private readonly Mock<IBookRepository> _bookRepository;
        private readonly Mock<IDapperBookRepository> _dapperBookRepository;
        private readonly IBookService _bookService;
        private readonly IMapper _mapper;
        private readonly Mock<IBookCatalogServiceClient> _bookCatalogServiceClient;

        public BookServiceTests()
        {
            _mapper = new MapperConfiguration(config =>
            {
                config.AddMaps(typeof(BookProfile));
            }).CreateMapper();
            _bookRepository = new Mock<IBookRepository>();
            _dapperBookRepository = new Mock<IDapperBookRepository>();
            _bookCatalogServiceClient = new Mock<IBookCatalogServiceClient>();
            _bookService = new BookService(_mapper, _bookRepository.Object, _dapperBookRepository.Object, _bookCatalogServiceClient.Object);
        }

        [Fact]
        public void GetAllBooks_Should_Return_Not_Empty_List()
        {
            // Arrage
            List<Book> books = new List<Book>()
            {
                new Book() { Author = "Pasha", Description = "Good book!", Name = "Harry Potter", Price = 500 },
                new Book() { Author = "Pasha1", Description = "Good book!1", Name = "Harry Potter1", Price = 600 },
                new Book() { Author = "Pasha2", Description = "Good book!2", Name = "Harry Potter2", Price = 700 }
            };
            _bookRepository.Setup(b => b.GetAll()).Returns(books.AsQueryable());

            // Act
            var resultBooks = _bookService.GetAllBooks();

            // Assert
            Assert.NotEmpty(resultBooks);
        }

        [Fact]
        public async Task AddBook_Result_Should_Be_Equal_To_Model()
        {
            // Arrage
            var bookModel = new AddBookModel() { Author = "Pasha", Description = "test", Name = "Book about love", Price = 200 };
            var bookEntity = _mapper.Map<Book>(bookModel);

            _bookRepository.Setup(b => b.AddAsync(bookEntity)).Returns(Task.FromResult(bookEntity));

            // Act
            var result = await _bookService.AddBookAsync(bookModel);

            // Accert
            Assert.Equal(bookModel.Name, result.Name);
        }

        [Fact]
        public async Task DeleteBook_Should_Return_Success_Result()
        {
            // Arrage
            Guid bookId = Guid.NewGuid();
            var book = new Book() { id = bookId, Author = "Pasha", Description = "test", Name = "Book about love", Price = 200 };

            _bookRepository.Setup(b => b.GetFirstAsync(It.IsAny<Expression<Func<Book, bool>>>())).Returns(Task.FromResult(book));

            // Act
            var result = await _bookService.DeleteBookAsync(bookId);

            // Assert
            Assert.Contains(result.status, "Success");
        }
    }
}