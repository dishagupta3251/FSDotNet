using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace BusTicketingApp.Repositories
{
    public class CustomerRepository : IRepository<Customer, int>
    {
        private readonly TicketingContext _ticketingContext;

        public CustomerRepository(TicketingContext ticketingContext)
        {
            _ticketingContext = ticketingContext;
        }

        public async Task<Customer> Add(Customer entity)
        {
            try
            {
                _ticketingContext.Customers.Add(entity);
                await _ticketingContext.SaveChangesAsync();
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
                var customerEntity = await Get(key);
                if (customerEntity != null)
                {
                    _ticketingContext.Customers.Remove(customerEntity);
                    await _ticketingContext.SaveChangesAsync();
                }
                return customerEntity;
            }
            catch
            {
                throw new NotFoundException("Customer");
            }
        }

        public async Task<Customer> Get(int key)
        {
            try
            {
                var customerEntity = await _ticketingContext.Customers
                    .FirstOrDefaultAsync(c => c.CustomerId == key);
                if (customerEntity == null) throw new Exception();
                return customerEntity;
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
                var customers = await _ticketingContext.Customers
                    .ToListAsync();
                if (customers.Count == 0) throw new Exception();
                return customers;
            }
            catch
            {
                throw new CollectionEmptyException("Customers");
            }
        }

        public async Task<Customer> Update(Customer entity, int key)
        {
            try
            {
                var existingCustomer = await Get(key);
                if (!string.IsNullOrEmpty(entity.CustomerName))
                {
                    existingCustomer.CustomerName = entity.CustomerName;
                }
                if (!string.IsNullOrEmpty(entity.Username))
                {
                    existingCustomer.Username = entity.Username;
                }
                if (entity.Age != 0) 
                {
                    existingCustomer.Age = entity.Age;
                }

                if (!string.IsNullOrEmpty(entity.City) && entity.City != existingCustomer.City)
                {
                    existingCustomer.City = entity.City;
                }


                if (!string.IsNullOrEmpty(entity.Contact) && entity.Contact != existingCustomer.Contact)
                {
                    existingCustomer.Contact = entity.Contact;
                }

                if (!string.IsNullOrEmpty(entity.Email) && entity.Email != existingCustomer.Email)
                {
                    existingCustomer.Email = entity.Email;
                }


                await _ticketingContext.SaveChangesAsync();

                return existingCustomer;
            }
            catch
            {
                throw new NotFoundException("Customer");
            }
        }
    }
}

