using AutoMapper;
using BookCatalog.Application.Models.Basket;
using BookCatalog.Application.Models.Book;
using BookCatalog.Core.Entities;

namespace BookCatalog.Application.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<BasketItem, BasketItemModel>();
            CreateMap<BasketItemModel, BasketItem>();
            CreateMap<Basket, BasketModel>();
            CreateMap<BasketModel, Basket>();
        }
    }
}
