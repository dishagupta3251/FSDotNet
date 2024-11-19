using BusTicketingApp.Contexts;
using Microsoft.EntityFrameworkCore;
using BusTicketingApp.Repositories;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UnitTesting.RepositoryTest
{
    public class SeatRepositoryTest
    {
        DbContextOptions options;
        TicketingContext context;
        SeatRepository repository;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase("TestSeatRepo")
                .Options;

            context = new TicketingContext(options);
            repository = new SeatRepository(context);
        }

        private Seats AddDetails()
        {
            return new Seats
            {
                SeatNumber = 23,
                IsBooked = false
            };
        }

        [Test]
        public async Task TestAdd()
        {
            var seat = AddDetails();
            var result = await repository.Add(seat);

            Assert.AreEqual(result.SeatNumber, seat.SeatNumber);
            Assert.IsFalse(result.IsBooked);
        }

        [Test]
        public void TestAddException()
        {
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(null));
        }

        [Test]
        public async Task TestDelete()
        {
            var seat = AddDetails();
            var addedSeat = await repository.Add(seat);

            var deletedSeat = await repository.Delete(addedSeat.SeatsId);
            Assert.AreEqual(deletedSeat.SeatsId, addedSeat.SeatsId);
        }

        [Test]
        public void TestDeleteException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete(0));
        }

        [Test]
        public async Task TestGet()
        {
            var seat = AddDetails();
            var addedSeat = await repository.Add(seat);

            var fetchedSeat = await repository.Get(addedSeat.SeatsId);
            Assert.AreEqual(fetchedSeat.SeatNumber, addedSeat.SeatNumber);
            Assert.AreEqual(fetchedSeat.SeatsId, addedSeat.SeatsId);
        }

        [Test]
        public void TestGetException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999));
        }

        [Test]
        public async Task GetAllTest()
        {
            context.Seats.RemoveRange(context.Seats); // Clear any existing seats.
            var seat = AddDetails();
            await repository.Add(seat);

            var seats = await repository.GetAll();
            Assert.IsNotEmpty(seats);
            Assert.AreEqual(seats.Count(), 1);
        }

        [Test]
        public void GetAllTestException()
        {
            context.Seats.RemoveRange(context.Seats); // Ensure no data in the context.
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task UpdateTest()
        {
            var seat = AddDetails();
            var addedSeat = await repository.Add(seat);

            addedSeat.IsBooked = true;
            addedSeat.SeatNumber = 12;
            var updatedSeat = await repository.Update(addedSeat, addedSeat.SeatsId);

            Assert.AreEqual(updatedSeat.SeatNumber, 12);
            Assert.IsTrue(updatedSeat.IsBooked);
        }

        [Test]
        public void UpdateTestException()
        {
            var invalidSeat = new Seats
            {
                SeatsId = 999,
                SeatNumber = 22,
                IsBooked = true
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(invalidSeat, invalidSeat.SeatsId));
        }
    }
}
