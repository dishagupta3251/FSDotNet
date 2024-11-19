using BusTicketingApp.Contexts;
using Microsoft.EntityFrameworkCore;
using BusTicketingApp.Repositories;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Models;


namespace UnitTesting.RepositoryTest
{
    public class BusRepositoryTest
    {
        DbContextOptions options;
        TicketingContext context;
        BusRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase("TestBusRepo")
                .Options;

            context = new TicketingContext(options);
            repository = new BusRepository(context);
        }

        private Bus AddDetails()
        {
            return new Bus
            {
                BusNumber = "B123",
                BusType = BusTypes.AC,
                Status = BusStatus.Regret,
                StandardFare = 500,
                PremiumFare = 1000
            };
        }

        [Test]
        public async Task TestAdd()
        {
            var bus = AddDetails();
            var result = await repository.Add(bus);

            Assert.AreEqual(result.BusNumber, bus.BusNumber);
            Assert.AreEqual(result.Status, bus.Status);
        }

        [Test]
        public void TestAddException()
        {
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(null));
        }

        [Test]
        public async Task TestDelete()
        {
            var bus = AddDetails();
            var addedBus = await repository.Add(bus);

            var deletedBus = await repository.Delete(addedBus.BusId);
            Assert.AreEqual(deletedBus.BusId, addedBus.BusId);
        }

        [Test]
        public void TestDeleteException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete(0));
        }

        [Test]
        public async Task TestGet()
        {
            var bus = AddDetails();
            var addedBus = await repository.Add(bus);

            var fetchedBus = await repository.Get(addedBus.BusId);
            Assert.AreEqual(fetchedBus.BusNumber, addedBus.BusNumber);
            Assert.AreEqual(fetchedBus.Status, addedBus.Status);
        }

        [Test]
        public void TestGetException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999));
        }

        [Test]
        public async Task GetAllTest()
        {
            context.Buses.RemoveRange(context.Buses); // Clear any existing buses.
            var bus = AddDetails();
            await repository.Add(bus);

            var buses = await repository.GetAll();
            Assert.IsNotEmpty(buses);
            Assert.AreEqual(buses.Count(), 1);
        }

        [Test]
        public void GetAllTestException()
        {
            context.Buses.RemoveRange(context.Buses); // Ensure no data in the context.
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task UpdateTest()
        {
            var bus = AddDetails();
            var addedBus = await repository.Add(bus);

            addedBus.Status = BusStatus.Running;
            addedBus.PremiumFare = 1200;
            var updatedBus = await repository.Update(addedBus, addedBus.BusId);

            Assert.AreEqual(updatedBus.Status,BusStatus.Running);
            Assert.AreEqual(updatedBus.PremiumFare, 1200);
        }

        [Test]
        public void UpdateTestException()
        {
            var invalidBus = new Bus
            {
                BusId = 999,
                BusNumber = "B999",
                Status = BusStatus.Running,
                PremiumFare = 1500
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(invalidBus, invalidBus.BusId));
        }
    }
}
