using BusTicketingApp.Contexts;
using BusTicketingApp.Repositories;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NUnit.Framework;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTesting.RepositoryTest
{
    public class BusOperatorRepositoryTest
    {
        private DbContextOptions options;
        private TicketingContext context;
        private BusOperatorRepository repository;
        private Mock<ILogger<BusOperatorRepository>> mockLogger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase("TestBusOperatorRepo")
                .Options;

            context = new TicketingContext(options);
            mockLogger = new Mock<ILogger<BusOperatorRepository>>();
            repository = new BusOperatorRepository(context, mockLogger.Object);
        }

        private BusOperator AddDetails()
        {
            return new BusOperator
            {
                OperatorName = "XYZ Transport",
                OperatorContact = "123-456-7890"
            };
        }

        [Test]
        public async Task TestAdd()
        {
            var busOperator = AddDetails();
            var result = await repository.Add(busOperator);

            Assert.AreEqual(result.OperatorName, busOperator.OperatorName);
            Assert.AreEqual(result.OperatorContact, busOperator.OperatorContact);
        }

        [Test]
        public void TestAddException()
        {
            var busOperator = new BusOperator { OperatorName = null, OperatorContact = null }; // Invalid data for adding
            mockLogger.Setup(x => x.LogError(It.IsAny<string>()));

            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(busOperator));
        }

        [Test]
        public async Task TestDelete()
        {
            var busOperator = AddDetails();
            var addedOperator = await repository.Add(busOperator);

            var deletedOperator = await repository.Delete(addedOperator.OperatorId);
            Assert.AreEqual(deletedOperator.OperatorId, addedOperator.OperatorId);
        }

        [Test]
        public void TestDeleteException()
        {
            mockLogger.Setup(x => x.LogError(It.IsAny<string>()));

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete(999));
        }

        [Test]
        public async Task TestGet()
        {
            var busOperator = AddDetails();
            var addedOperator = await repository.Add(busOperator);

            var fetchedOperator = await repository.Get(addedOperator.OperatorId);
            Assert.AreEqual(fetchedOperator.OperatorName, addedOperator.OperatorName);
        }

        [Test]
        public void TestGetException()
        {
            mockLogger.Setup(x => x.LogError(It.IsAny<string>()));

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999));
        }

        [Test]
        public async Task GetAllTest()
        {
            context.BusOperators.RemoveRange(context.BusOperators); // Clear any existing operators
            var busOperator = AddDetails();
            await repository.Add(busOperator);

            var operators = await repository.GetAll();
            Assert.IsNotEmpty(operators);
            Assert.AreEqual(operators.Count(), 1);
        }

        [Test]
        public void GetAllTestException()
        {
            context.BusOperators.RemoveRange(context.BusOperators); // Ensure no data in the context
            mockLogger.Setup(x => x.LogError(It.IsAny<string>()));

            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task UpdateTest()
        {
            var busOperator = AddDetails();
            var addedOperator = await repository.Add(busOperator);

            addedOperator.OperatorName = "Updated Transport";
            addedOperator.OperatorContact = "987-654-3210";
            var updatedOperator = await repository.Update(addedOperator, addedOperator.OperatorId);

            Assert.AreEqual(updatedOperator.OperatorName, "Updated Transport");
            Assert.AreEqual(updatedOperator.OperatorContact, "987-654-3210");
        }

        [Test]
        public void UpdateTestException()
        {
            var invalidOperator = new BusOperator
            {
                OperatorId = 999,
                OperatorName = "Invalid Operator",
                OperatorContact = "000-000-0000"
            };
            mockLogger.Setup(x => x.LogError(It.IsAny<string>()));

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(invalidOperator, invalidOperator.OperatorId));
        }
    }
}
