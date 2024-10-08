using Microsoft.AspNetCore.Mvc;
using PizzaStoreAPI.Interface;

namespace PizzaStoreAPI.Controllers
{
    public class OrderController
    {
        [Route("api/[controller]")]
        [ApiController]
        public class OrderController : ControllerBase
        {
            private readonly IOrderService _orderService;

            public OrderController(IOrderService orderService)
            {
                _orderService = orderService;
            }

            [HttpPost]
            [Route("placeOrder")]
            public async Task<IActionResult> PlaceOrder(int customerId)
            {
                var order = await _orderService.PlaceOrder(customerId);
                return Ok(order);
            }

            [HttpGet]
            [Route("getOrderDetails")]
            public async Task<IActionResult> GetOrderDetails(int orderNumber)
            {
                var orderDetails = await _orderService.GetOrderDetails(orderNumber);
                return Ok(orderDetails);
            }
        }

    }
}
