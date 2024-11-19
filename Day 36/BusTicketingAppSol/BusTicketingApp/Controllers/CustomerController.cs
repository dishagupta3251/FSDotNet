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

       
        [HttpPost("CreateCustomer")]
        [Authorize(Roles = "Customer")]
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

        [HttpPatch("UpdateCustomerProfile")]

        [Authorize(Roles = "Customer")]
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

       
        [HttpGet("GetCustomerById")]
        [Authorize(Roles = "Admin,BusOperator")]
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

        [HttpGet("GetAllCustomer")]
        [Authorize(Roles = "Admin,BusOperator")]
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
