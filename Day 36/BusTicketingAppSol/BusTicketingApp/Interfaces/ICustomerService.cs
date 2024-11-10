using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface ICustomerService
    {
        Task<Customer> AddCustomer(CustomerCreateDTO customerCreateDTO);
        Task<Customer> UpdateCustomer(int id, CustomerCreateDTO customerCreateDTO);
        Task<Customer> GetCustomerById(int id);
        Task<IEnumerable<Customer>> GetAllCustomers();
    }
}
