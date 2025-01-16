
using BankingCustomerManagement.Context;
using BankingCustomerManagement.Exceptions;
using BankingCustomerManagement.Interfaces;
using BankingCustomerManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class CustomerRepository : IRepository<Customer, int>
{
    private readonly BankingContext _bankingContext;
    private readonly ILogger<CustomerRepository> _logger;

    public CustomerRepository(BankingContext bankingContext, ILogger<CustomerRepository> logger)
    {
        _bankingContext = bankingContext;
        _logger = logger;
    }

    public async Task<Customer> Add(Customer entity)
    {
        try
        {
            _bankingContext.Customers.Add(entity);
            await _bankingContext.SaveChangesAsync();
            _logger.LogInformation("Added a new Customer with {CustomerId}.", entity.CustId);
            return entity;
        }
        catch
        {
            throw new CouldNotAddException("Customer");
        }
    }

    public async Task<Customer> Delete(int key)
    {
        try
        {
            var customer = await Get(key);
            if (customer != null)
            {
                _bankingContext.Customers.Remove(customer);
                await _bankingContext.SaveChangesAsync();
                _logger.LogInformation("Deleting customer with ID {CustomerId} entity.", key);
            }
            else
            {
                throw new Exception();
            }
            return customer;
        }
        catch
        {
            throw new CouldNotDeleteException("Customer");
        }
    }

    public async Task<Customer> Get(int key)
    {
        try
        {
            var customer = await _bankingContext.Customers.FirstOrDefaultAsync(c => c.CustId == key);
            _logger.LogInformation("Getting customer with customer ID {CustomerId}.", key);
            if (customer == null) throw new Exception();
            return customer;
        }
        catch
        {
            throw new NotFoundException("Customer");
        }
    }

    public async Task<IEnumerable<Customer>> GetAll()
    {
        try
        {
            var customers = await _bankingContext.Customers.ToListAsync();
            _logger.LogInformation("Getting all the customers");

            if (customers.Count == 0)
            {
                _logger.LogInformation("No customers found. Count:{Count}", customers.Count);
                throw new CollectionEmptyException("Customers");
            }

            return customers;
        }
        catch (CollectionEmptyException ex)
        {
            _logger.LogWarning(ex.Message);
            throw;
        }
        
    }

    public async Task<Customer> Update(Customer entity, int key)
    {
        try
        {
            var existingCustomer = await Get(key);
            if (existingCustomer != null)
            {
                if (!string.IsNullOrWhiteSpace(entity.FirstName))
                {
                    existingCustomer.FirstName = entity.FirstName;
                }
                if (!string.IsNullOrWhiteSpace(entity.LastName))
                {
                    existingCustomer.LastName = entity.LastName;
                }

                if (!string.IsNullOrWhiteSpace(entity.Email))
                {
                    existingCustomer.Email = entity.Email;
                }

                if (!string.IsNullOrWhiteSpace(entity.Address))
                {
                    existingCustomer.Address = entity.Address;
                }

                if (!string.IsNullOrWhiteSpace(entity.PhoneNumber))
                {
                    existingCustomer.PhoneNumber = entity.PhoneNumber;
                }

                if (!string.IsNullOrWhiteSpace(entity.City))
                {
                    existingCustomer.City = entity.City;
                }

            }
                await _bankingContext.SaveChangesAsync();
                _logger.LogInformation("Updating customer with ID {CustomerId}", key);
                return existingCustomer;
                
           
        
  

        }
        catch
        {
            throw new CouldNotUpdateException("Customer");
        }
    }
}
