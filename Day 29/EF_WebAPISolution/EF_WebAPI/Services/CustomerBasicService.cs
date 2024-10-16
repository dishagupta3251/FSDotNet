using EF_WebAPI.Interfaces;
using EF_WebAPI.Models;
using EF_WebAPI.Models.DTO;

namespace EF_WebAPI.Services
{
    public class CustomerBasicService : ICustomerBasicService
    {
        private readonly IRepository<int, Customer> _customerRepository;

        public CustomerBasicService(IRepository<int, Customer> customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<int> CreateCustomer(CustomerDTO customer)
        {
            Customer newCustomer = MapCustomerDTOToCustomer(customer);
            newCustomer.Age = CalculateAgeFromDateTime(customer.DateOfBirth);
            var addedCustomer = await _customerRepository.Add(newCustomer);
            return addedCustomer.Id;
        }

        private Customer MapCustomerDTOToCustomer(CustomerDTO customer)
        {
            return new Customer
            {
                Name = customer.Name,
                Email = customer.Email,
                Phone = customer.Phone,
                DateOfBirth = customer.DateOfBirth
            };
        }

        int CalculateAgeFromDateTime(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var age = today.Year - dateOfBirth.Year;
            if (dateOfBirth.Date > today.AddYears(-age)) age--;
            return age;
        }

      
    }

}
