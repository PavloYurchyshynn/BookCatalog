using AutoMapper;
using BookCatalog.Application.Helpers;
using BookCatalog.Application.Models.Basket;
using BookCatalog.Application.Services.Contracts;
using BookCatalog.Core.Entities;
using BookCatalog.DataAccess.Repositories.Contracts;

namespace BookCatalog.Application.Services
{
    public class BasketService : IBasketService
    {
        private readonly IBasketRepository _basketRepository;
        private readonly IBasketItemRepository _basketItemRepository;
        private readonly IMapper _mapper;
        private readonly IBookRepository _bookRepository;

        public BasketService(
            IBasketRepository basketRepository, 
            IBasketItemRepository basketItemRepository, 
            IMapper mapper, 
            IBookRepository bookRepository)
        {
            _basketRepository = basketRepository;
            _basketItemRepository = basketItemRepository;
            _mapper = mapper;
            _bookRepository = bookRepository;
        }

        public async Task<BasketModel> AddItemToBasketAsync(BasketItemModel model)
        {
            var itemEntity = _mapper.Map<BasketItem>(model);

            var basket = await _basketRepository.GetFirstAsync(b => b.id != Guid.Empty);
            if (basket == null)
            {
                Basket basketEntity = new Basket();

                await _basketRepository.AddAsync(basketEntity);

                basket = await _basketRepository.GetFirstAsync(b => b.id != Guid.Empty);
            }

            basket.TotalQuantity += itemEntity.Quantity;
            var book = await _bookRepository.GetFirstAsync(b => b.id == itemEntity.BookId);

            if (book == null) 
            {
                throw new Exception();
            }

            basket.TotalAmount += book.Price * itemEntity.Quantity;
            itemEntity.BasketId = basket.id;
            await _basketRepository.UpdateAsync(basket);
            await _basketItemRepository.AddAsync(itemEntity);

            return _mapper.Map<BasketModel>(basket);
        }

        public IEnumerable<BasketItemModel> GetBasketItems(Guid basketId)
        {
            var basketItems = _basketItemRepository.GetWhere(i => i.BasketId == basketId).ToList();

            if (basketItems.Count == 0)
            {
                throw new Exception("Not found");
            }

            return _mapper.Map<IEnumerable<BasketItemModel>>(basketItems);
        }

        public async Task<BasketItemModel> UpdateBasketItemAsync(UpdateBasketItem model, Guid itemId)
        {
            var item = await _basketItemRepository.GetFirstAsync(i => i.id == itemId);
            var basket = await _basketRepository.GetFirstAsync(b => b.id == item.BasketId);
            var book = await _bookRepository.GetFirstAsync(b => b.id == item.BookId);

            if (item == null || basket == null || book == null)
            {
                throw new Exception("Not found");
            }

            basket.TotalAmount = basket.TotalAmount - (book.Price * item.Quantity) + (book.Price * model.Quantity);
            basket.TotalQuantity = basket.TotalQuantity - item.Quantity + model.Quantity;
            item.Quantity = model.Quantity;

            await _basketRepository.UpdateAsync(basket);
            var updatedItem = await _basketItemRepository.UpdateAsync(item);

            return _mapper.Map<BasketItemModel>(updatedItem);
        }

        public async Task<Response> DeleteBasketItemAsync(Guid itemId)
        {
            var item = await _basketItemRepository.GetFirstAsync(b => b.id == itemId);
            var basket = await _basketRepository.GetFirstAsync(b => b.id == item.BasketId);
            var book = await _bookRepository.GetFirstAsync(b => b.id == item.BookId);

            if (item == null || basket == null || book == null)
            {
                throw new Exception("Not found");
            }

            basket.TotalAmount -= (book.Price * item.Quantity);
            basket.TotalQuantity -= item.Quantity;

            await _basketRepository.UpdateAsync(basket);
            await _basketItemRepository.DeleteAsync(item);

            return new Response { status = "Success", message = "Item deleted!" };
        }
    }
}
