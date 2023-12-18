using BookCatalog.Application.Helpers;
using BookCatalog.Application.Models.Basket;

namespace BookCatalog.Application.Services.Contracts
{
    public interface IBasketService
    {
        Task<BasketModel> AddItemToBasketAsync(BasketItemModel model);
        IEnumerable<BasketItemModel> GetBasketItems(Guid basketId);
        Task<BasketItemModel> UpdateBasketItemAsync(UpdateBasketItem model, Guid itemId);
        Task<Response> DeleteBasketItemAsync(Guid itemId);
    }

}
