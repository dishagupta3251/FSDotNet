using EF_WebAPI.Interfaces;
using EF_WebAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EF_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerBasicService _customerService;

        public CustomerController(ICustomerBasicService customerService)
        {
            _customerService = customerService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDTO customer)
        {
            try
            {
                var customerId = await _customerService.CreateCustomer(customer);
                return Ok(customerId);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
