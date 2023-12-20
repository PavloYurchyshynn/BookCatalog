using BookCatalog.Application.Models.Basket;
using BookCatalog.Application.Models.Book;
using BookCatalog.Application.Services;
using BookCatalog.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.API.Controllers
{
    [Route("api/basket")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasketAsync(BasketItemModel model)
        {
            try
            {
                var basket = await _basketService.AddItemToBasketAsync(model);
                return Ok(basket);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{basketId}")]
        public IActionResult GetBusketItems(Guid basketId)
        {
            try
            {
                var items = _basketService.GetBasketItems(basketId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("{itemId}")]
        public async Task<IActionResult> UpdateBasketItemAsync(UpdateBasketItem model, Guid itemId)
        {
            try
            {
                var items = await _basketService.UpdateBasketItemAsync(model, itemId);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{itemId}")]
        public async Task<IActionResult> DeleteBasketItemAsync(Guid itemId)
        {
            try
            {
                var response = await _basketService.DeleteBasketItemAsync(itemId);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
