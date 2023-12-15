using AutoMapper;
using BookCatalog.Application.Models.Book;
using BookCatalog.Core.Entities;

namespace BookCatalog.Application.MappingProfiles
{
    public class BookProfile : Profile
    {
        public BookProfile() 
        {
            CreateMap<Book, BookModel>();
            CreateMap<BookModel, Book>();
            CreateMap<Book, AddBookModel>();
            CreateMap<AddBookModel, Book>();
            CreateMap<Book, UpdateBookModel>();
            CreateMap<UpdateBookModel, Book>();
        }
    }
}
