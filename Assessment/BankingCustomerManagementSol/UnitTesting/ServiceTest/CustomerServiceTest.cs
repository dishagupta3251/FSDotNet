using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BankingCustomerManagement.Exceptions;
using BankingCustomerManagement.Interfaces;
using BankingCustomerManagement.Models;
using BankingCustomerManagement.Models.DTO;
using BankingCustomerManagement.Services;
using Moq;

namespace UnitTesting.ServiceTest
{
    public class CustomerServiceTest
    {
        private readonly Mock<IRepository<Customer, int>> _mockCustomerRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly CustomerService _customerService;

        public CustomerServiceTest()
        {
            _mockCustomerRepository = new Mock<IRepository<Customer, int>>();
            _mockMapper = new Mock<IMapper>();
            _customerService = new CustomerService(_mockCustomerRepository.Object, _mockMapper.Object);
        }

        private Customer Details()
        {
            return new Customer
            {
                CustId = 1,
                FirstName = "Disha",
                LastName = "Gupta",
                Email = "disha.gupta@example.com",
                DateOfBirth = DateTime.Now,
                City = "Lucknow",
                PhoneNumber = "0987654321",

            };
        }
        private List<Customer> CustomerList()
        {
            return new List<Customer> { Details() };
        }
        [Test]
        public async Task AddCustomerTest()
        {
            
            var customerDTO = new CustomerDTO
            {
                FirstName = "Disha",
                LastName = "Gupta",
                Email = "disha.gupta@example.com",
                DateOfBirth = DateTime.Now,
                City="Lucknow",
                PhoneNumber="0987654321"

            };

            var customer = new Customer
            {
                CustId = 1,
                FirstName = "Disha",
                LastName = "Gupta",
                Email = "disha.gupta@example.com",
                DateOfBirth = DateTime.Now,
                City = "Lucknow",
                PhoneNumber = "0987654321",

            };

            _mockMapper.Setup(m => m.Map<Customer>(customerDTO)).Returns(customer);
            _mockCustomerRepository.Setup(repo => repo.Add(It.IsAny<Customer>())).ReturnsAsync(customer);

            
            var result = await _customerService.AddCustomer(customerDTO);

            
            Assert.NotNull(result);
            Assert.AreEqual(customer.CustId, result.CustId);
            Assert.AreEqual(customer.FirstName, result.FirstName);
        }

        [Test]
        public void AddCustomerException()
        {
            var customerDTO = new CustomerDTO { FirstName = "Disha" };
            _mockMapper.Setup(m => m.Map<Customer>(customerDTO)).Throws(new Exception());

            Assert.ThrowsAsync<CouldNotAddException>(async () => await _customerService.AddCustomer(customerDTO));
        }

        [Test]
        public async Task GetCustomerByIdTest()
        {
            var customer = Details();
            _mockCustomerRepository.Setup(repo => repo.Get(1)).ReturnsAsync(customer);
            var result = await _customerService.GetCustomerById(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.CustId);
        }
        [Test]
        public void GetCustomerByIdException()
        {
            _mockCustomerRepository.Setup(repo => repo.Get(1)).Throws(new Exception());

            Assert.ThrowsAsync<NotFoundException>(async () => await _customerService.GetCustomerById(1));
        }

        [Test]
        public async Task GetAllTest()
        {
            var customers = CustomerList();
            _mockCustomerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(customers);

            var result = await _customerService.GetAll();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.Count());
        }

        [Test]
        public void GetAllException()
        {
            _mockCustomerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Customer>());

            Assert.ThrowsAsync<Exception>(async () => await _customerService.GetAll());
        }

        [Test]
        public async Task UpdateCustomerTest()
        {
            var existingCustomer = Details();
            var updatedCustomerDTO = new CustomerDTO { FirstName = "Updated" };

            _mockCustomerRepository.Setup(repo => repo.Get(1)).ReturnsAsync(existingCustomer);
            _mockCustomerRepository.Setup(repo => repo.Update(It.IsAny<Customer>(), 1))
                .ReturnsAsync(existingCustomer);

            var result = await _customerService.UpdateCustomer(1, updatedCustomerDTO);

            Assert.IsNotNull(result);
            Assert.AreEqual("Updated", existingCustomer.FirstName);
        }

        [Test]
        public void UpdateCustomerException()
        {
            var customerDTO = new CustomerDTO { FirstName = "Updated" };
            _mockCustomerRepository.Setup(repo => repo.Get(1)).Throws(new Exception());

            Assert.ThrowsAsync<CouldNotUpdateException>(async () => await _customerService.UpdateCustomer(1, customerDTO));
        }

        [Test]
        public async Task DeleteCustomerTest()
        {
            var customer = Details();
            _mockCustomerRepository.Setup(repo => repo.Delete(1)).ReturnsAsync(customer);

            var result = await _customerService.DeleteCustomer(1);

            Assert.IsNotNull(result);
            Assert.AreEqual(customer.CustId, result.CustId);
        }

        [Test]
        public void DeleteCustomerException()
        {
            _mockCustomerRepository.Setup(repo => repo.Delete(1)).Throws(new Exception());

            Assert.ThrowsAsync<CouldNotDeleteException>(async () => await _customerService.DeleteCustomer(1));
        }
        [Test]
        public async Task GetCustomerByFirstNameTest()
        {
            var customer = Details();
            customer.FirstName=customer.FirstName.ToLower();
            var firstName = "Disha";
            _mockCustomerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Customer> { customer});

            var result = await _customerService.GetCustomerByFirstName(firstName);

            Assert.IsNotNull(result);
            Assert.AreEqual("disha", result.FirstName);
        }

        [Test]
        public void GetCustomerByFirstNameException()
        {
            var firstName = "ABC";
            var customer = Details();
            customer.FirstName = customer.FirstName.ToLower();
            _mockCustomerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Customer> { customer});

            Assert.ThrowsAsync<NotFoundException>(async () => await _customerService.GetCustomerByFirstName(firstName));
        }

        [Test]
        public async Task GetCustomerByLastNameTest()
        {
            var customer = Details();
            customer.LastName=customer.LastName.ToLower();
            var lastName = "Gupta";
            _mockCustomerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Customer> { customer });

            var result = await _customerService.GetCustomerByLastName(lastName);

            Assert.IsNotNull(result);
            Assert.AreEqual(customer.LastName, result.LastName);
        }

        [Test]
        public void GetCustomerByLastNameException()
        {
            var lastName = "XYZ";
            var customer = Details();
            customer.FirstName = customer.FirstName.ToLower();
            _mockCustomerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Customer> { customer });
           

            Assert.ThrowsAsync<NotFoundException>(async () => await _customerService.GetCustomerByLastName(lastName));
        }

        [Test]
        public async Task GetCustomerByAccountNumberTest()
        {
            var customer = Details();
            customer.AccountNumber = "1234567890";
            var accountNumber = "1234567890";
            _mockCustomerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Customer> { customer });

            var result = await _customerService.GetCustomerByAccountNumber(accountNumber);

            Assert.IsNotNull(result);
            Assert.AreEqual(customer.AccountNumber, result.AccountNumber);
        }

        [Test]
        public void GetCustomerByAccountNumberException()
        {
            var accountNumber = "1234562229032";
            var customer = Details();
            _mockCustomerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Customer> { customer});

            Assert.ThrowsAsync<NotFoundException>(async () => await _customerService.GetCustomerByAccountNumber(accountNumber));
        }

        [Test]
        public async Task GetCustomerByPhoneNumberTest()
        {
            var customer = Details();
            var phoneNumber = "0987654321";
            _mockCustomerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Customer> { customer });

            var result = await _customerService.GetCustomerByPhoneNumber(phoneNumber);

            Assert.IsNotNull(result);
            Assert.AreEqual(customer.PhoneNumber, result.PhoneNumber);
        }

        [Test]
        public void GetCustomerByPhoneNumberException()
        {
            var phoneNumber = "0987654331";
            var customer = Details();
            _mockCustomerRepository.Setup(repo => repo.GetAll()).ReturnsAsync(new List<Customer> { customer});

            Assert.ThrowsAsync<NotFoundException>(async () => await _customerService.GetCustomerByPhoneNumber(phoneNumber));
        }

    }
}



