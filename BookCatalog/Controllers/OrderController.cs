using BookCatalog.Application.Models.Order;
using BookCatalog.Application.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace BookCatalog.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> MakeOrderAsync(MakeOrderModel model)
        {
            try
            {
                var order = await _orderService.MakeOrderAsync(model);
                return Ok(order);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
