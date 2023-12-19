using AutoMapper;
using BookCatalog.Application.Models.Basket;
using BookCatalog.Application.Models.Order;
using BookCatalog.Core.Entities;

namespace BookCatalog.Application.MappingProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<Order, OrderModel>();
            CreateMap<OrderModel, Order>();
        }
    }
}
