using Microsoft.AspNetCore.Mvc;
using ThanachartTest.Domain.AggregatesModel.CartAggregate;
using ThanachartTest.Domain.AggregatesModel.CartAggregate.Interfaces;
using ThanachartTest.Domain.Services;

namespace ThanachartTest.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var cart = await _cartService.GetActiveCartAsync();
            return Ok(cart);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            try
            {
                var cart = await _cartService.AddToCartAsync(request);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateCartItem([FromBody] UpdateCartItemRequest request)
        {
            try
            {
                var cart = await _cartService.UpdateCartItemAsync(request);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveCartItem(Guid cartItemId)
        {
            try
            {
                var cart = await _cartService.RemoveCartItemAsync(cartItemId);
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            try
            {
                var cart = await _cartService.ClearCartAsync();
                return Ok(cart);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
