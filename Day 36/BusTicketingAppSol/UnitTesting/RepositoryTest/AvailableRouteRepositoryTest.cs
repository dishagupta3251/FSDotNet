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
    public class AvailableRouteRepositoryTest
    {
        private DbContextOptions options;
        private TicketingContext context;
        private AvailableRouteRepository repository;
        private Mock<ILogger<AvailableRouteRepository>> mockLogger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase("TestAvailableRouteRepo")
                .Options;

            context = new TicketingContext(options);
            mockLogger = new Mock<ILogger<AvailableRouteRepository>>();
            repository = new AvailableRouteRepository(context, mockLogger.Object);
        }

        private AvailableRoute AddDetails()
        {
            return new AvailableRoute
            {
                Origin = "City A",
                Destination = "City B"
            };
        }

        [Test]
        public async Task TestAdd()
        {
            var availableRoute = AddDetails();
            var result = await repository.Add(availableRoute);

            Assert.AreEqual(result.Origin, availableRoute.Origin);
            Assert.AreEqual(result.Destination, availableRoute.Destination);
        }

        [Test]
        public void TestAddException()
        {
            var availableRoute = new AvailableRoute { Origin = null, Destination = null }; // Invalid data for adding
            mockLogger.Setup(x => x.LogError(It.IsAny<string>()));

            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(availableRoute));
        }

        [Test]
        public async Task TestDelete()
        {
            var availableRoute = AddDetails();
            var addedRoute = await repository.Add(availableRoute);

            var deletedRoute = await repository.Delete(addedRoute.RouteId);
            Assert.AreEqual(deletedRoute.RouteId, addedRoute.RouteId);
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
            var availableRoute = AddDetails();
            var addedRoute = await repository.Add(availableRoute);

            var fetchedRoute = await repository.Get(addedRoute.RouteId);
            Assert.AreEqual(fetchedRoute.Origin, addedRoute.Origin);
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
            context.AvailableRoutes.RemoveRange(context.AvailableRoutes); // Clear any existing routes
            var availableRoute = AddDetails();
            await repository.Add(availableRoute);

            var routes = await repository.GetAll();
            Assert.IsNotEmpty(routes);
            Assert.AreEqual(routes.Count(), 1);
        }

        [Test]
        public void GetAllTestException()
        {
            context.AvailableRoutes.RemoveRange(context.AvailableRoutes); // Ensure no data in the context
            mockLogger.Setup(x => x.LogError(It.IsAny<string>()));

            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task UpdateTest()
        {
            var availableRoute = AddDetails();
            var addedRoute = await repository.Add(availableRoute);

            addedRoute.Origin = "Updated City A";
            addedRoute.Destination = "Updated City B";
            var updatedRoute = await repository.Update(addedRoute, addedRoute.RouteId);

            Assert.AreEqual(updatedRoute.Origin, "Updated City A");
            Assert.AreEqual(updatedRoute.Destination, "Updated City B");
        }

        [Test]
        public void UpdateTestException()
        {
            var invalidRoute = new AvailableRoute
            {
                RouteId = 999,
                Origin = "Invalid Origin",
                Destination = "Invalid Destination"
            };
            mockLogger.Setup(x => x.LogError(It.IsAny<string>()));

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(invalidRoute, invalidRoute.RouteId));
        }
    }
}
