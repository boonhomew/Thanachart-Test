using Microsoft.AspNetCore.Mvc;
using ThanachartTest.Domain.AggregatesModel.OrderAggregate.Interfaces;
using ThanachartTest.Domain.Services;

namespace ThanachartTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var result = await _orderService.CheckoutAsync();
            
            if (result.Status == "SUCCESS")
                return Ok(result);
            
            return BadRequest(result);
        }
    }
}
