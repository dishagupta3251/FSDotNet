using Microsoft.AspNetCore.Mvc;
using PizzaStoreAPI.Exceptions;
using PizzaStoreAPI.Interface;

namespace PizzaStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController:ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService) {
            _customerService = customerService;
        }
        [HttpGet("ViewPizzas")]
        public async Task<IActionResult> ViewPizzas()
        {
            try
            {
                var pizzas = await _customerService.GetAllPizzas();
                return Ok(pizzas);
            }
            catch (CollectionEmptyException e)
            {

                return NotFound(e.Message);
            }
            catch (Exception e)
            {
               
                return BadRequest(e.Message);
            }
        }
        [HttpGet("ViewToppings")]
        public async Task<IActionResult> ViewToppings()
        {
            try
            {
                var toppings = await _customerService.GetAllToppings();
                return Ok(toppings);
            }
            catch (CollectionEmptyException e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
