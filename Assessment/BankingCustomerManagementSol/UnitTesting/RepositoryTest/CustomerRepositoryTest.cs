using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingCustomerManagement.Context;
using BankingCustomerManagement.Exceptions;
using BankingCustomerManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace UnitTesting.RepositoryTest
{
    public class CustomerRepositoryTest
    {

        DbContextOptions options;
        BankingContext context;
        CustomerRepository repository;
        ILogger<CustomerRepository> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<BankingContext>()
                .UseInMemoryDatabase("TestBookingRepo")
                .Options;

            context = new BankingContext(options);
            logger = new LoggerFactory().CreateLogger<CustomerRepository>();
            repository = new CustomerRepository(context, logger);
        }
        private Customer AddCustomerDetails()
        {
            return new Customer
            {
                FirstName = "Test1",
                LastName ="Test2",
                Email = "abc@gmail.com",
                City="XYZ",
                DateOfBirth=DateTime.Now,
                AccountNumber="78CJHS393HH",
                PhoneNumber = "0987654321",
                

            };
        }

        [Test]
        public async Task AddTest()
        {
            var customer= AddCustomerDetails();
            var result = await repository.Add(customer);
            Assert.AreEqual(customer.FirstName, customer.FirstName);
            Assert.AreEqual(customer.LastName, customer.LastName);
        }
        [Test]
        public void TestAddException()
        {
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(null));
        }
        [Test]
        public async Task TestDelete()
        {
            var customer = AddCustomerDetails();
            var addedCustomer = await repository.Add(customer);

            var deletedCustomer = await repository.Delete(addedCustomer.CustId);
            Assert.AreEqual(deletedCustomer.CustId, addedCustomer.CustId);
        }

        [Test]
        public void TestDeleteException()
        {
            Assert.ThrowsAsync<CouldNotDeleteException>(async () => await repository.Delete(999));
        }

        [Test]
        public async Task TestGet()
        {
            var customer = AddCustomerDetails();
            var addedCustomer = await repository.Add(customer);
            var fetchedCustomer = await repository.Get(addedCustomer.CustId);
            Assert.AreEqual(fetchedCustomer.FirstName, addedCustomer.FirstName);
        }

        [Test]
        public void TestGetException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999));
        }

        [Test]
        public async Task GetAllTest()
        {
            context.Customers.RemoveRange(context.Customers); 
            var customer = AddCustomerDetails();
            await repository.Add(customer);

            var customers = await repository.GetAll();
            Assert.IsNotEmpty(customers);
        }

        [Test]
        public void GetAllTestException()
        {
            context.Customers.RemoveRange(context.Customers); 
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task UpdateTest()
        {
            var customer = AddCustomerDetails();
            var addedCustomer = await repository.Add(customer);

            customer.FirstName = "UpdatedName";
            var updatedCustomer = await repository.Update(customer, addedCustomer.CustId);

            Assert.AreEqual(updatedCustomer.FirstName, "UpdatedName");
        }

        [Test]
        public void UpdateTestException()
        {
            var invalidCustomer = new Customer
            {
                
                FirstName = "Name"
            };

            Assert.ThrowsAsync<CouldNotUpdateException>(async () => await repository.Update(invalidCustomer, 999));
        }
    }
}

