using AutoMapper;
using BookCatalog.Application.Models.Order;
using BookCatalog.Application.Services.Contracts;
using BookCatalog.Core.Entities;
using BookCatalog.DataAccess.Repositories.Contracts;

namespace BookCatalog.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IOrderItemRepository _orderItemRepository;
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IOrderRepository orderRepository, 
            IOrderItemRepository orderItemRepository, 
            IBasketRepository basketRepository,
            IBasketItemRepository basketItemRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
            _mapper = mapper;
        }

        public async Task<OrderModel> MakeOrderAsync(MakeOrderModel model)
        {
            var basketItems = _basketItemRepository.GetWhere(i => i.BasketId == model.BasketId).ToList();
            var basket = await _basketRepository.GetFirstAsync(b => b.id == model.BasketId);

            if (basketItems.Count <= 0)
            {
                throw new Exception("Basket items not found");
            }

            var order = new Order
            {
                Name = model.Name,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Price = basket.TotalAmount,
                BasketId = model.BasketId,
                DeliveryAddress = model.DeliveryAddress,
                OrderStatus = "Preparing for delivery",
            };

            var addedOrder = await _orderRepository.AddAsync(order);

            foreach (var item in basketItems)
            {
                var orderItem = new OrderItem
                {
                    BookId = item.BookId,
                    OrderId = addedOrder.id,
                    Quantity = item.Quantity,
                };

                await _orderItemRepository.AddAsync(orderItem);
                await _basketItemRepository.DeleteAsync(item);
            }

            basket.TotalQuantity = 0;
            basket.TotalAmount = 0;

            await _basketRepository.UpdateAsync(basket);

            return _mapper.Map<OrderModel>(addedOrder);
        }
    }
}
