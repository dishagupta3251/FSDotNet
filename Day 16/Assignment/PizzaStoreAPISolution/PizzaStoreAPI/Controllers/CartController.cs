using Microsoft.AspNetCore.Mvc;
using PizzaStoreAPI.Interface;
using PizzaStoreAPI.Models.DTOs;

namespace PizzaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost]
        [Route("addPizzaToCart")]
        public async Task<IActionResult> AddPizzaToCart([FromBody] PizzaCartDTO pizzaDTO, int customerId)
        {
            var cart = await _cartService.AddPizzaToCart(pizzaDTO, customerId);
            return Ok(cart);
        }

        [HttpGet]
        [Route("getCart")]
        public async Task<IActionResult> GetCart(int customerId)
        {
            var cart = await _cartService.GetCart(customerId);
            return Ok(cart);
        }

        [HttpPut]
        [Route("updateCart")]
        public async Task<IActionResult> UpdateCart([FromBody] PizzaCartDTO pizzaDTO, int customerId)
        {
            var updatedCart = await _cartService.UpdateCart(pizzaDTO, customerId);
            return Ok(updatedCart);
        }
    }

}
