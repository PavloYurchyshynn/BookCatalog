using BookCatalog.Application.Models.Order;

namespace BookCatalog.Application.Services.Contracts
{
    public interface IOrderService
    {
        Task<OrderModel> MakeOrderAsync(MakeOrderModel model);
    }
}
