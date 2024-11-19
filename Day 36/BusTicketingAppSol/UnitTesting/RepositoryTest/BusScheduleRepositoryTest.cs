using System.Linq;
using System.Threading.Tasks;
using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Models;
using BusTicketingApp.Repositories;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace UnitTesting.RepositoryTest
{
    public class BusScheduleRepositoryTest
    {
        private DbContextOptions<TicketingContext> _options;
        private TicketingContext _context;
        private BusScheduleRepository _repository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase("TestBusScheduleRepo")
                .Options;

            _context = new TicketingContext(_options);
            _repository = new BusScheduleRepository(_context);
        }

        private BusSchedule CreateBusSchedule()
        {
            var route = new AvailableRoute { RouteId = 1, Origin="Route1", Destination="Route2" };
            var bus = new Bus { BusId = 1, BusNumber = "B123", BusType = BusTypes.AC, Status = BusStatus.Running };
            _context.AvailableRoutes.Add(route);
            _context.Buses.Add(bus);
            _context.SaveChanges();

            return new BusSchedule
            {
                BusId = bus.BusId,
                RouteId = route.RouteId,
                Day = DaysOfWeek.Monday
            };
        }

        [Test]
        public async Task TestAdd()
        {
            var busSchedule = CreateBusSchedule();
            var result = await _repository.Add(busSchedule);

            Assert.AreEqual(busSchedule.Day, result.Day);
            Assert.AreEqual(busSchedule.BusId, result.BusId);
            Assert.AreEqual(busSchedule.RouteId, result.RouteId);
        }

        [Test]
        public void TestAddException()
        {
            Assert.ThrowsAsync<CouldNotAddException>(async () => await _repository.Add(null));
        }

        [Test]
        public async Task TestDelete()
        {
            var busSchedule = CreateBusSchedule();
            var addedBusSchedule = await _repository.Add(busSchedule);

            var deletedBusSchedule = await _repository.Delete(addedBusSchedule.BusScheduleId);

            Assert.AreEqual(deletedBusSchedule.BusScheduleId, addedBusSchedule.BusScheduleId);
        }

        [Test]
        public void TestDeleteException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Delete(0));
        }

        [Test]
        public async Task TestGet()
        {
            var busSchedule = CreateBusSchedule();
            var addedBusSchedule = await _repository.Add(busSchedule);

            var fetchedBusSchedule = await _repository.Get(addedBusSchedule.BusScheduleId);

            Assert.AreEqual(fetchedBusSchedule.BusScheduleId, addedBusSchedule.BusScheduleId);
            Assert.AreEqual(fetchedBusSchedule.Day, addedBusSchedule.Day);
        }

        [Test]
        public void TestGetException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(999));
        }

        [Test]
        public async Task TestGetAll()
        {
            var busSchedule = CreateBusSchedule();
            await _repository.Add(busSchedule);

            var schedules = await _repository.GetAll();

            Assert.IsNotEmpty(schedules);
            Assert.AreEqual(schedules.Count(), 1);
        }

        [Test]
        public void TestGetAllException()
        {
            _context.BusSchedules.RemoveRange(_context.BusSchedules); // Ensure no data
            _context.SaveChanges();
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await _repository.GetAll());
        }

        [Test]
        public async Task TestUpdate()
        {
            var busSchedule = CreateBusSchedule();
            var addedBusSchedule = await _repository.Add(busSchedule);

            addedBusSchedule.Day = DaysOfWeek.Friday;

            var updatedBusSchedule = await _repository.Update(addedBusSchedule, addedBusSchedule.BusScheduleId);

            Assert.AreEqual(updatedBusSchedule.Day, DaysOfWeek.Friday);
        }

        [Test]
        public void TestUpdateException()
        {
            var invalidBusSchedule = new BusSchedule
            {
                BusScheduleId = 999,
                BusId = 999,
                Day = DaysOfWeek.Saturday,
                RouteId = 999
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Update(invalidBusSchedule, invalidBusSchedule.BusScheduleId));
        }
    }
}
