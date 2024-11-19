using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Models;
using BusTicketingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace UnitTesting.RepositoryTest
{
    public class CustomerRepositoryTest
    {
        private DbContextOptions<TicketingContext> _options;
        private TicketingContext _context;
        private CustomerRepository _repository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase("TestCustomerRepo")
                .Options;

            _context = new TicketingContext(_options);
            _repository = new CustomerRepository(_context);
        }

        private Customer CreateTestCustomer()
        {
            return new Customer
            {
                CustomerName = "John Doe",
                Age = 30,
                City = "New York",
                Contact = "1234567890",
                Email = "johndoe@example.com"
            };
        }

        [Test]
        public async Task AddCustomer_ShouldAddSuccessfully()
        {
            // Arrange
            var customer = CreateTestCustomer();

            // Act
            var result = await _repository.Add(customer);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(customer.CustomerName, result.CustomerName);
            Assert.AreEqual(customer.Email, result.Email);

            // Verify database state
            var dbCustomer = await _context.Customers.FindAsync(result.CustomerId);
            Assert.NotNull(dbCustomer);
            Assert.AreEqual(customer.Contact, dbCustomer.Contact);
        }

        [Test]
        public void AddCustomer_ShouldThrowCouldNotAddException_WhenAddingFails()
        {
            // Arrange
            var customer = (Customer)null;

            // Act & Assert
            Assert.ThrowsAsync<CouldNotAddException>(async () => await _repository.Add(customer));
        }

        [Test]
        public async Task DeleteCustomer_ShouldDeleteSuccessfully()
        {
            // Arrange
            var customer = CreateTestCustomer();
            var addedCustomer = await _repository.Add(customer);

            // Act
            var deletedCustomer = await _repository.Delete(addedCustomer.CustomerId);

            // Assert
            Assert.NotNull(deletedCustomer);
            Assert.AreEqual(addedCustomer.CustomerId, deletedCustomer.CustomerId);

            // Verify database state
            var dbCustomer = await _context.Customers.FindAsync(deletedCustomer.CustomerId);
            Assert.Null(dbCustomer);
        }

        [Test]
        public void DeleteCustomer_ShouldThrowNotFoundException_WhenCustomerDoesNotExist()
        {
            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Delete(999));
        }

        [Test]
        public async Task GetCustomer_ShouldReturnCustomerSuccessfully()
        {
            // Arrange
            var customer = CreateTestCustomer();
            var addedCustomer = await _repository.Add(customer);

            // Act
            var fetchedCustomer = await _repository.Get(addedCustomer.CustomerId);

            // Assert
            Assert.NotNull(fetchedCustomer);
            Assert.AreEqual(addedCustomer.CustomerId, fetchedCustomer.CustomerId);
        }

        [Test]
        public void GetCustomer_ShouldThrowNotFoundException_WhenCustomerDoesNotExist()
        {
            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(999));
        }

        [Test]
        public async Task GetAllCustomers_ShouldReturnAllCustomers()
        {
            // Arrange
            var customer = CreateTestCustomer();
            await _repository.Add(customer);

            // Act
            var customers = await _repository.GetAll();

            // Assert
            Assert.NotNull(customers);
            Assert.IsNotEmpty(customers);
            Assert.AreEqual(1, customers.Count());
        }

        [Test]
        public void GetAllCustomers_ShouldThrowCollectionEmptyException_WhenNoCustomersExist()
        {
            // Act & Assert
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await _repository.GetAll());
        }

        [Test]
        public async Task UpdateCustomer_ShouldUpdateSuccessfully()
        {
            // Arrange
            var customer = CreateTestCustomer();
            var addedCustomer = await _repository.Add(customer);

            // Modify customer
            var updatedCustomer = new Customer
            {
                CustomerName = "Jane Doe",
                Age = 28,
                City = "Los Angeles",
                Contact = "9876543210",
                Email = "janedoe@example.com"
            };

            // Act
            var result = await _repository.Update(updatedCustomer, addedCustomer.CustomerId);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual("Jane Doe", result.CustomerName);
            Assert.AreEqual("Los Angeles", result.City);
            Assert.AreEqual(28, result.Age);
            Assert.AreEqual("9876543210", result.Contact);
            Assert.AreEqual("janedoe@example.com", result.Email);

            // Verify database state
            var dbCustomer = await _context.Customers.FindAsync(result.CustomerId);
            Assert.NotNull(dbCustomer);
            Assert.AreEqual("Jane Doe", dbCustomer.CustomerName);
        }

        [Test]
        public void UpdateCustomer_ShouldThrowNotFoundException_WhenCustomerDoesNotExist()
        {
            // Arrange
            var nonExistentCustomer = new Customer
            {
                CustomerId = 999,
                CustomerName = "Nonexistent User"
            };

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Update(nonExistentCustomer, nonExistentCustomer.CustomerId));
        }

        [TearDown]
        public void Teardown()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
