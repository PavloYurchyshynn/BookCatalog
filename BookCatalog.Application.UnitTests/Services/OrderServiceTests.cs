using AutoMapper;
using BookCatalog.Application.MappingProfiles;
using BookCatalog.Application.Models.Order;
using BookCatalog.Application.Services;
using BookCatalog.Application.Services.Contracts;
using BookCatalog.Core.Entities;
using BookCatalog.DataAccess.Repositories.Contracts;
using Moq;
using System.Linq.Expressions;

namespace BookCatalog.Application.UnitTests.Services
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _orderRepository;
        private readonly Mock<IOrderItemRepository> _orderItemRepository;
        private readonly Mock<IBasketRepository> _basketRepository;
        private readonly Mock<IBasketItemRepository> _basketItemRepository;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;

        public OrderServiceTests()
        {
            _orderRepository = new Mock<IOrderRepository>();
            _orderItemRepository = new Mock<IOrderItemRepository>();
            _basketRepository = new Mock<IBasketRepository>();
            _basketItemRepository = new Mock<IBasketItemRepository>();
            _mapper = new MapperConfiguration(config =>
            {
                config.AddMaps(typeof(OrderProfile));
            }).CreateMapper();
            _orderService = new OrderService(_orderRepository.Object, _orderItemRepository.Object, _basketRepository.Object, _basketItemRepository.Object, _mapper);
        }

        [Fact]
        public async Task MakeOrder_Should_Return_Order()
        {
            // Arrage

            Guid basketId = Guid.NewGuid();
            var orderModel = new MakeOrderModel() { BasketId = basketId, DeliveryAddress = "Home", Email = "test@x.com", Name = "Pasha", PhoneNumber = 23432432 };
            var basketItems = new List<BasketItem>()
            {
                new BasketItem() { BookId = Guid.NewGuid(), Quantity = 5, BasketId = basketId },
                new BasketItem() { BookId = Guid.NewGuid(), Quantity = 5, BasketId = basketId },
                new BasketItem() { BookId = Guid.NewGuid(), Quantity = 5, BasketId = basketId },
            };
            var basket = new Basket() { id = basketId, TotalQuantity = 15, TotalAmount = 1500 };
            var order = new Order()
            {
                BasketId = basketId,
                DeliveryAddress = orderModel.DeliveryAddress,
                Email = orderModel.Email,
                Name = orderModel.Name,
                PhoneNumber = orderModel.PhoneNumber,
                Price = basket.TotalAmount
            };
            var addedOrder = new Order()
            {
                id = Guid.NewGuid(),
                BasketId = order.BasketId,
                DeliveryAddress = order.DeliveryAddress,
                Email = order.Email,
                Name = order.Name,
                PhoneNumber = order.PhoneNumber,
                Price = order.Price
            };
            var query = basketItems.Where(x => x.BasketId == basketId).AsQueryable();

            _basketItemRepository.Setup(x => x.GetWhere(It.IsAny<Expression<Func<BasketItem, bool>>>())).Returns(query);
            _basketRepository.Setup(x => x.GetFirstAsync(It.IsAny<Expression<Func<Basket, bool>>>())).Returns(Task.FromResult(basket));
            _orderRepository.Setup(x => x.AddAsync(order)).Returns(Task.FromResult(addedOrder));
            foreach (var item in basketItems)
            {
                var orderItem = new OrderItem()
                {
                    BookId = item.BookId,
                    OrderId = addedOrder.id,
                    Quantity = item.Quantity,
                };

                _orderItemRepository.Setup(x => x.AddAsync(orderItem));
                _basketItemRepository.Setup(x => x.DeleteAsync(item));
            }
            _basketRepository.Setup(x => x.UpdateAsync(basket));

            // Act
            var result = await _orderService.MakeOrderAsync(orderModel);

            // Assert
            Assert.NotNull(result);

        }
    }
}