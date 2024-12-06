using BusTicketingApp.Controllers;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UnitTesting.ControllersTest
{
    [TestFixture]
    public class CustomersControllerTest
    {
        private Mock<ICustomerService> _mockCustomerService;
        private CustomersController _controller;

        [SetUp]
        public void Setup()
        {
            _mockCustomerService = new Mock<ICustomerService>();
            _controller = new CustomersController(_mockCustomerService.Object);
        }

        [Test]
        public async Task AddCustomer_ShouldReturnOk_WhenCustomerIsCreatedSuccessfully()
        {
            // Arrange
            var customerCreateDTO = new CustomerCreateDTO
            {
                Username = "TestUser",
                Contact = "1234567890",
                Email = "testuser@test.com",
                City = "TestCity",
                Age = 25
            };

            var createdCustomer = new Customer
            {
                CustomerId = 1,
                Username = "TestUser",
                Contact = "1234567890",
                Email = "testuser@test.com",
                City = "TestCity",
                Age = 25
            };

            _mockCustomerService.Setup(s => s.AddCustomer(customerCreateDTO)).ReturnsAsync(createdCustomer);

            // Act
            var result = await _controller.AddCustomer(customerCreateDTO);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as dynamic;
            Assert.NotNull(response);
            Assert.AreEqual(1, response.id);
        }

        [Test]
        public async Task UpdateCustomer_ShouldReturnOk_WhenCustomerIsUpdatedSuccessfully()
        {
            // Arrange
            var customerId = 1;
            var customerUpdateDTO = new CustomerCreateDTO
            {
                Username = "UpdatedUser",
                Contact = "0987654321",
                Email = "updated@test.com",
                City = "UpdatedCity",
                Age = 30
            };

            var updatedCustomer = new Customer
            {
                CustomerId = customerId,
                Username = "UpdatedUser",
                Contact = "0987654321",
                Email = "updated@test.com",
                City = "UpdatedCity",
                Age = 30
            };

            _mockCustomerService.Setup(s => s.UpdateCustomer(customerId, customerUpdateDTO)).ReturnsAsync(updatedCustomer);

            // Act
            var result = await _controller.UpdateCustomer(customerId, customerUpdateDTO);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as Customer;
            Assert.NotNull(response);
            Assert.AreEqual("UpdatedUser", response.Username);
        }

        [Test]
        public async Task GetCustomerById_ShouldReturnOk_WhenCustomerExists()
        {
            // Arrange
            var customerId = 1;
            var customer = new Customer
            {
                CustomerId = customerId,
                Username = "TestUser",
                Contact = "1234567890",
                Email = "testuser@test.com",
                City = "TestCity",
                Age = 25
            };

            _mockCustomerService.Setup(s => s.GetCustomerById(customerId)).ReturnsAsync(customer);

            // Act
            var result = await _controller.GetCustomerById(customerId);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as Customer;
            Assert.NotNull(response);
            Assert.AreEqual(customerId, response.CustomerId);
        }

        [Test]
        public async Task GetAllCustomers_ShouldReturnOk_WhenCustomersExist()
        {
            // Arrange
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, Username = "User1", Contact = "1234567891", Email = "user1@test.com", City = "City1", Age = 20 },
                new Customer { CustomerId = 2, Username = "User2", Contact = "1234567892", Email = "user2@test.com", City = "City2", Age = 22 }
            };

            _mockCustomerService.Setup(s => s.GetAllCustomers()).ReturnsAsync(customers);

            // Act
            var result = await _controller.GetAllCustomers();

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as IEnumerable<Customer>;
            Assert.NotNull(response);
            Assert.AreEqual(2, response.Count());
        }

        [Test]
        public async Task GetCustomerByUsername_ShouldReturnOk_WhenCustomerExists()
        {
            // Arrange
            var username = "TestUser";
            var customer = new Customer
            {
                CustomerId = 1,
                Username = username,
                Contact = "1234567890",
                Email = "testuser@test.com",
                City = "TestCity",
                Age = 25
            };

            _mockCustomerService.Setup(s => s.GetCustomerByUsername(username)).ReturnsAsync(customer);

            // Act
            var result = await _controller.GetCustomerByUsername(username);

            // Assert
            var okResult = result as OkObjectResult;
            Assert.NotNull(okResult);
            Assert.AreEqual(200, okResult.StatusCode);

            var response = okResult.Value as Customer;
            Assert.NotNull(response);
            Assert.AreEqual(username, response.Username);
        }
    }
}
