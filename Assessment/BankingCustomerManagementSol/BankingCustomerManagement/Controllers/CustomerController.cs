
using BankingCustomerManagement.Interfaces;
using BankingCustomerManagement.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankingCustomerManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        
        [HttpPost]
     
        public async Task<IActionResult> AddCustomer( CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var addedCustomer = await _customerService.AddCustomer(customerDTO);
                return Ok(addedCustomer.CustId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("{custId}")]
        
        public async Task<IActionResult> DeleteCustomer(int custId)
        {
            try
            {
                var deletedCustomer = await _customerService.DeleteCustomer(custId);
                return Ok(deletedCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetAll")]
        
        public async Task<IActionResult> GetAllCustomers()
        {
            try
            {
                var customers = await _customerService.GetAll();
                return Ok(customers);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("/customerId/{custId}")]
      
        public async Task<IActionResult> GetCustomerById(int custId)
        {
            try
            {
                var customer = await _customerService.GetCustomerById(custId);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("/firstname/{firstName}")]
        
        public async Task<IActionResult> FetchCustomerByFirstName(string firstName)
        {
            try
            {
                var customer = await _customerService.GetCustomerByFirstName(firstName);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("/lastname/{lastName}")]
       
        public async Task<IActionResult> FetchCustomerByLastName(string lastName)
        {
            try
            {
                var customer = await _customerService.GetCustomerByLastName(lastName);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("/phone/{phoneNumber}")]
        public async Task<IActionResult> FetchCustomerByPhoneNumber(string phoneNumber)
        {
            try
            {
                var customer=await _customerService.GetCustomerByPhoneNumber(phoneNumber);
                return Ok(customer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet("/account/{accountNumber}")]
        public async Task<IActionResult> FetchCustomerByAccount(string accountNumber)
        {
            try { 
                var customer=await _customerService.GetCustomerByAccountNumber(accountNumber);
                return Ok(customer);
            } catch (Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut("{custId}")]
        
        public async Task<IActionResult> UpdateCustomer(int custId, CustomerDTO customerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedCustomer = await _customerService.UpdateCustomer(custId, customerDTO);
                return Ok(updatedCustomer);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
