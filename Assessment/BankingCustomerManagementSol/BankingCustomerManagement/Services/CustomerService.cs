using System.CodeDom;
using System.Linq.Expressions;
using AutoMapper;
using BankingCustomerManagement.Exceptions;
using BankingCustomerManagement.Interfaces;
using BankingCustomerManagement.Models;
using BankingCustomerManagement.Models.DTO;

namespace BankingCustomerManagement.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository<Customer, int> _customerRepository;
        private readonly IMapper _mapper;

        public CustomerService(IRepository<Customer, int> customerRepository, IMapper mapper)
        {
            _customerRepository = customerRepository;
            _mapper = mapper;
        }
        private string GenerateAccountNumber()
        {

            int length = 13;
            Random random = new Random();

            string accountNumber = string.Empty;
            for (int i = 0; i < length; i++)
            {
                accountNumber += random.Next(0, 10);
            }

            return accountNumber;
        }
        public async Task<Customer> AddCustomer(CustomerDTO customerDTO)
        {
            var mssg = "";
            try
            {
                customerDTO.FirstName = customerDTO.FirstName.ToLower();
                customerDTO.LastName = customerDTO.LastName.ToLower();
                var customer = _mapper.Map<Customer>(customerDTO);
                customer.AccountNumber = GenerateAccountNumber();
                var exsisting_customer = (await GetAll()).FirstOrDefault(c => c.PhoneNumber == customer.PhoneNumber);
                if (exsisting_customer != null){
                    mssg = "Phone number already exists";
                    throw new Exception();
                }
                var addedCustomer = await _customerRepository.Add(customer);
                mssg="Customer";
                return addedCustomer;
            }
            catch
            {
                throw new CouldNotAddException(mssg);
            }
        }

        public async Task<Customer> DeleteCustomer(int customerId)
        {
            try
            {
                return await _customerRepository.Delete(customerId);
            }
            catch
            {
                throw new CouldNotDeleteException("Customer");
            }
        }

        public async Task<IEnumerable<Customer>> GetAll()
        {
            try
            {
                var customers = await _customerRepository.GetAll();

                if (customers.Count() == 0) throw new Exception("Collection empty");
                return customers;
            }
            catch (Exception ex) 
            {
                    throw new Exception(ex.Message);
            }
        }

        public async Task<Customer> GetCustomerById(int custId)
        {
            try
            {
                return await _customerRepository.Get(custId);
            }
            catch
            {
                throw new NotFoundException("Customer");
            }
        }

        public async Task<Customer> UpdateCustomer(int custId, CustomerDTO customerDTO)
        {
            try
            {
                var existingCustomer = await _customerRepository.Get(custId);

                existingCustomer.FirstName = customerDTO.FirstName ?? existingCustomer.FirstName;
                existingCustomer.LastName = customerDTO.LastName ?? existingCustomer.LastName;
                existingCustomer.Email = customerDTO.Email ?? existingCustomer.Email;
                existingCustomer.PhoneNumber = customerDTO.PhoneNumber ?? existingCustomer.PhoneNumber;
                existingCustomer.Address = customerDTO.Address ?? existingCustomer.Address;
                existingCustomer.City = customerDTO.City ?? existingCustomer.City;

                return await _customerRepository.Update(existingCustomer, custId);
            }
            catch
            {
                throw new CouldNotUpdateException("Customer");
            }
        }

        public async Task<Customer> GetCustomerByFirstName(string firstName)
        {
            try
            {
                firstName = firstName.ToLower();
                var customer = (await GetAll()).FirstOrDefault(c => c.FirstName == firstName);
                if (customer == null)
                {
                    throw new Exception();
                }
                return customer;

            }
            catch (Exception ex)
            {
                {
                    throw new NotFoundException(firstName);
                }
            }
        }

            public async Task<Customer> GetCustomerByLastName(string lastName)
            {
                try
                {
                lastName = lastName.ToLower();
                var customer = (await GetAll()).FirstOrDefault(c => c.LastName == lastName);
                if (customer == null)
                {
                
                    throw new Exception();
                }
                return customer;
            }
                catch (Exception ex) {
                throw new NotFoundException(lastName);
            }
            }

            public async Task<Customer> GetCustomerByAccountNumber(string accountNumber)
            {
            var customer = (await GetAll()).FirstOrDefault(c => c.AccountNumber == accountNumber);
            if (customer == null)
            {
                throw new NotFoundException(accountNumber);
            }
            return customer;
        }

            public async Task<Customer> GetCustomerByPhoneNumber(string phoneNumber)
            {
            var customer = (await GetAll()).FirstOrDefault(c => c.PhoneNumber == phoneNumber);
            if (customer == null)
            {
                throw new NotFoundException(phoneNumber);
            }
            return customer;
        }
        }
    }

