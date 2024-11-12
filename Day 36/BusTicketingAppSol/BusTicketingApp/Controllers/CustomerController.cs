using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusTicketingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

       // [Authorize(Roles ="Customer")]
        [HttpPost]
        public async Task<ActionResult<Customer>> AddCustomer(CustomerCreateDTO customerCreateDTO)
        {
            try
            {
                var customer = await _customerService.AddCustomer(customerCreateDTO);
                return Ok(new { id = customer.CustomerId });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

       // [Authorize(Roles ="Customer")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerCreateDTO customerCreateDTO)
        {
            try
            {
                var updatedCustomer = await _customerService.UpdateCustomer(id, customerCreateDTO);
                return Ok(updatedCustomer);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

       // [Authorize(Roles ="Admin,BusOperator")]
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetCustomerById(int id)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(id);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

      //  [Authorize(Roles = "Admin,BusOperator")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Customer>>> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAllCustomers();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }
    }
}
