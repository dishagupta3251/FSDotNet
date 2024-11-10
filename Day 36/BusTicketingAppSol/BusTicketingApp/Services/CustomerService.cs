using AutoMapper;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using BusTicketingApp.Repositories;
using Microsoft.Extensions.Logging;

namespace BusTicketingApp.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer,int> _customerRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerService> _logger;

        public CustomerService(IRepository<Customer,int> customerRepository, IMapper mapper, ILogger<CustomerService> logger)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Customer> AddCustomer(CustomerCreateDTO customerCreateDTO)
        {
            try
            {
                _logger.LogInformation("Starting to add a new customer.");
                var customer = _mapper.Map<Customer>(customerCreateDTO); 
                var addedCustomer = await _customerRepository.Add(customer);
                _logger.LogInformation($"Successfully added new customer: {addedCustomer.CustomerName}");
                return addedCustomer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding customer: {ex.Message}");
                throw new Exception("An error occurred while adding the customer.");
            }
        }

   
        public async Task<Customer> UpdateCustomer(int id, CustomerCreateDTO customerCreateDTO)
        {
            try
            {
                _logger.LogInformation($"Starting to update customer with ID: {id}");
                var existingCustomer = await _customerRepository.Get(id);
                if (existingCustomer == null)
                {
                    _logger.LogWarning($"Customer with ID {id} not found for update.");
                    throw new Exception("Customer not found.");
                }

             
                _mapper.Map(customerCreateDTO, existingCustomer);
                var updatedCustomer = await _customerRepository.Update(existingCustomer, id);

                _logger.LogInformation($"Successfully updated customer with ID: {id}");
                return updatedCustomer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while updating customer: {ex.Message}");
                throw new Exception("An error occurred while updating the customer.");
            }
        }

   
        public async Task<Customer> GetCustomerById(int id)
        {
            try
            {
                _logger.LogInformation($"Starting to fetch customer with ID: {id}");
                var customer = await _customerRepository.Get(id);
                if (customer == null)
                {
                    _logger.LogWarning($"Customer with ID {id} not found.");
                    throw new Exception("Customer not found.");
                }
                _logger.LogInformation($"Successfully fetched customer with ID: {id}");
                return customer;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching customer: {ex.Message}");
                throw new Exception("An error occurred while fetching the customer.");
            }
        }


        public async Task<IEnumerable<Customer>> GetAllCustomers()
        {
            try
            {
                _logger.LogInformation("Starting to fetch all customers.");
                var customers = await _customerRepository.GetAll();
                if (customers == null || !customers.Any())
                {
                    _logger.LogWarning("No customers found.");
                    throw new Exception("No customers found.");
                }
                _logger.LogInformation($"Successfully fetched {customers.Count()} customers.");
                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while fetching customers: {ex.Message}");
                throw new Exception("An error occurred while fetching customers.");
            }
        }
    }
}
