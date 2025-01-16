using BankingCustomerManagement.Models;
using BankingCustomerManagement.Models.DTO;

namespace BankingCustomerManagement.Interfaces
{
    public interface ICustomerService
    {
        public Task<Customer> AddCustomer(CustomerDTO customerDTO);
        public Task<Customer> DeleteCustomer(int customerId);
        public Task<Customer> UpdateCustomer(int custId, CustomerDTO customerDTO);
        public Task<Customer> GetCustomerByFirstName(string firstName);
        public Task<Customer> GetCustomerByLastName(string lastName);
        public Task<Customer> GetCustomerByAccountNumber(string accountNumber);

        public Task<Customer> GetCustomerByPhoneNumber(string phoneNumber);
        public Task<IEnumerable<Customer>> GetAll();
        public Task<Customer> GetCustomerById(int custId);
    }
}
