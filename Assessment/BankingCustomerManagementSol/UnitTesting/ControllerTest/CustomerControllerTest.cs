using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingCustomerManagement.Controllers;
using BankingCustomerManagement.Interfaces;
using BankingCustomerManagement.Models.DTO;
using BankingCustomerManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace UnitTesting.ControllerTest
{
    public class CustomerControllerTest
    {
        private Mock<ICustomerService> _mockCustomerService;
        private CustomerController _controller;

        [SetUp]
        public void Setup()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _controller = new CustomerController(_mockCustomerService.Object);
        }

        private Customer GetCustomerDetails()
        {
            return new Customer
            {
                CustId = 1,
                FirstName = "disha",
                LastName = "gupta",
                Email = "disha.gupta@example.com",
                DateOfBirth = DateTime.Now,
                City = "New York",
                PhoneNumber = "1234567890",
                AccountNumber = "9876543210"
            };
        }

        private List<Customer> GetCustomerList()
        {
            return new List<Customer> { GetCustomerDetails() };
        }

        [Test]
        public async Task AddCustomerControllerTest()
        {
            var customerDTO = new CustomerDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                DateOfBirth = System.DateTime.Now,
                City = "New York",
                PhoneNumber = "1234567890"
            };

            var addedCustomer = GetCustomerDetails();

            _mockCustomerService.Setup(s => s.AddCustomer(customerDTO)).ReturnsAsync(addedCustomer);

            var result = await _controller.AddCustomer(customerDTO) as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual(addedCustomer.CustId, result.Value);
        }
        [Test]
        public async Task AddCustomerModelTest()
        {
            
            var customerDTO = new CustomerDTO();
            _controller.ModelState.AddModelError("FirstName", "First Name is required");

            
            var result = await _controller.AddCustomer(customerDTO);

           
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);
           
        }
        [Test]
        public async Task AddCustomerControllerException()
        {
           
            var customerDTO = new CustomerDTO
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john.doe@example.com",
                DateOfBirth = DateTime.Now.AddYears(-30),
                City = "New York",
                PhoneNumber = "1234567890"
            };

            _mockCustomerService
                .Setup(service => service.AddCustomer(It.IsAny<CustomerDTO>()))
                .ThrowsAsync(new Exception("Internal server error"));

            
            var result = await _controller.AddCustomer(customerDTO);

            
            Assert.IsInstanceOf<ObjectResult>(result);
            var objectResult = result as ObjectResult;
            Assert.IsNotNull(objectResult);
            Assert.AreEqual(500, objectResult.StatusCode);
            
        }


        [Test]
        public async Task GetAllCustomersControllerTest()
        {
            var customers = GetCustomerList();

            _mockCustomerService.Setup(s => s.GetAll()).ReturnsAsync(customers);

            var result = await _controller.GetAllCustomers() as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<IEnumerable<Customer>>(result.Value);
        }

        [Test]
        public async Task GetAllCustomersControllerTestException()
        {
            _mockCustomerService.Setup(s => s.GetAll()).ThrowsAsync(new Exception("Error occurred"));

            var result = await _controller.GetAllCustomers() as ObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual("Error occurred", result.Value);
        }

        [Test]
        public async Task GetCustomerByIdControllerTest()
        {
            var customer = GetCustomerDetails();

            _mockCustomerService.Setup(s => s.GetCustomerById(1)).ReturnsAsync(customer);

            var result = await _controller.GetCustomerById(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<Customer>(result.Value);
        }

        [Test]
        public async Task GetCustomerByIdControllerTestException()
        {
            _mockCustomerService.Setup(s => s.GetCustomerById(1)).ThrowsAsync(new Exception("Error occurred"));

            var result = await _controller.GetCustomerById(1) as ObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(500, result.StatusCode);
            Assert.AreEqual("Error occurred", result.Value);
        }

        [Test]
        public async Task DeleteCustomerControllerTest()
        {
            var customer = GetCustomerDetails();

            _mockCustomerService.Setup(s => s.DeleteCustomer(1)).ReturnsAsync(customer);

            var result = await _controller.DeleteCustomer(1) as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<Customer>(result.Value);
        }
        [Test]
        public async Task DeleteCustomerException()
        {
           
            _mockCustomerService.Setup(service => service.DeleteCustomer(1)).ThrowsAsync(new Exception("Internal server error"));

            var result = await _controller.DeleteCustomer(1) as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        
        }
        [Test]
        public async Task FetchCustomerByFirstNameControllerTest()
        {
            var customer = GetCustomerDetails();

            _mockCustomerService.Setup(s => s.GetCustomerByFirstName("Disha")).ReturnsAsync(customer);

            var result = await _controller.FetchCustomerByFirstName("Disha") as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<Customer>(result.Value);
        }
        [Test]
        public async Task FetchCustomerByFirstNameException()
        {
            _mockCustomerService.Setup(service => service.GetCustomerByFirstName(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Error fetching customer by first name"));

            var result = await _controller.FetchCustomerByFirstName("Disha") as ObjectResult;
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async Task FetchCustomerByLastNameControllerTest()
        {
            var customer = GetCustomerDetails();

            _mockCustomerService.Setup(s => s.GetCustomerByLastName("Gupta")).ReturnsAsync(customer);

            var result = await _controller.FetchCustomerByLastName("Gupta") as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<Customer>(result.Value);
        }
        [Test]
        public async Task FetchCustomerByLastNameException()
        {
            _mockCustomerService.Setup(service => service.GetCustomerByLastName(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Error fetching customer by last name"));

            var result = await _controller.FetchCustomerByLastName("Gupta") as ObjectResult ;

           
            Assert.AreEqual(500, result.StatusCode);
        }

        [Test]
        public async Task FetchCustomerByPhoneNumberControllerTest()
        {
            var customer = GetCustomerDetails();

            _mockCustomerService.Setup(s => s.GetCustomerByPhoneNumber("1234567890")).ReturnsAsync(customer);

            var result = await _controller.FetchCustomerByPhoneNumber("1234567890") as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<Customer>(result.Value);
        }
        [Test]
        public async Task FetchCustomerByPhoneNumberException()
        {
            _mockCustomerService.Setup(service => service.GetCustomerByPhoneNumber(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Error fetching customer by phone number"));

            var result = await _controller.FetchCustomerByPhoneNumber("1234567890") as ObjectResult;

           
            Assert.AreEqual(500, result.StatusCode);
        }
        [Test]
        public async Task FetchCustomerByAccountControllerTest()
        {
            var customer = GetCustomerDetails();

            _mockCustomerService.Setup(s => s.GetCustomerByAccountNumber("9876543210")).ReturnsAsync(customer);

            var result = await _controller.FetchCustomerByAccount("9876543210") as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.IsInstanceOf<Customer>(result.Value);
        }
        [Test]
        public async Task FetchCustomerByAccountException()
        {
            _mockCustomerService.Setup(service => service.GetCustomerByAccountNumber(It.IsAny<string>()))
                .ThrowsAsync(new Exception("Error fetching customer by account number"));

            var result = await _controller.FetchCustomerByAccount("123456789") as ObjectResult;

            
            Assert.AreEqual(500, result.StatusCode);
        }


        [Test]
        public async Task UpdateCustomerControllerTest()
        {
            var customerDTO = new CustomerDTO { FirstName = "Updated" };
            var updatedCustomer = GetCustomerDetails();
            updatedCustomer.FirstName = "Updated";

            _mockCustomerService.Setup(s => s.UpdateCustomer(1, customerDTO)).ReturnsAsync(updatedCustomer);

            var result = await _controller.UpdateCustomer(1, customerDTO) as OkObjectResult;

            Assert.NotNull(result);
            Assert.AreEqual(200, result.StatusCode);
            Assert.AreEqual("Updated", (result.Value as Customer).FirstName);
        }
        [Test]
        public async Task UpdateCustomerException()
        {
            var customerDTO = new CustomerDTO { FirstName = "John" };
            _mockCustomerService.Setup(service => service.UpdateCustomer(It.IsAny<int>(), It.IsAny<CustomerDTO>()))
                .ThrowsAsync(new Exception("Error updating customer"));

            var result = await _controller.UpdateCustomer(1, customerDTO) as ObjectResult;

          
            Assert.AreEqual(500, result.StatusCode);
        }
        [Test]
        public async Task UpdateCustomerModelTest()
        {

            var customerDTO = new CustomerDTO();
            _controller.ModelState.AddModelError("FirstName", "First Name is required");


            var result = await _controller.UpdateCustomer(1,customerDTO);


            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.IsNotNull(badRequestResult);
            Assert.AreEqual(400, badRequestResult.StatusCode);

        }
    }
}
