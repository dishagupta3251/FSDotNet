using System;
using System.Collections.Generic;
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
    public class SeatsBookedRepositoryTest
    {
        private TicketingContext _context;
        private SeatsBookedRepository _repository;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase(databaseName: "BusTicketingAppTestDb")
                .Options;

            _context = new TicketingContext(options);
            _repository = new SeatsBookedRepository(_context);
        }

        private SeatsBooked CreateSeatBooking()
        {
            return new SeatsBooked
            {
                Id = 1,
                BusId = 101,
                SeatId = 10,
                CustomerId = 2001,
                BookingId = 3001,
                SeatStatus = Status.Confirmed
            };
        }

        [Test]
        public async Task TestAdd()
        {
            var seatBooking = CreateSeatBooking();
            var result = await _repository.Add(seatBooking);

            Assert.AreEqual(seatBooking.Id, result.Id);
            Assert.AreEqual(seatBooking.SeatStatus, result.SeatStatus);
            Assert.AreEqual(seatBooking.BusId, result.BusId);
        }

        [Test]
        public void TestAddException()
        {
            var seatBooking = (SeatsBooked)null;
            Assert.ThrowsAsync<CouldNotAddException>(async () => await _repository.Add(seatBooking));
        }

        [Test]
        public async Task TestDelete()
        {
            var seatBooking = CreateSeatBooking();
            await _context.SeatsBooked.AddAsync(seatBooking);
            await _context.SaveChangesAsync();

            var deletedSeat = await _repository.Delete(seatBooking.Id);

            Assert.AreEqual(seatBooking.Id, deletedSeat.Id);
        }

        [Test]
        public void TestDeleteException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Delete(999));
        }

        [Test]
        public async Task TestGet()
        {
            var seatBooking = CreateSeatBooking();
            await _context.SeatsBooked.AddAsync(seatBooking);
            await _context.SaveChangesAsync();

            var fetchedSeatBooking = await _repository.Get(seatBooking.Id);

            Assert.AreEqual(seatBooking.Id, fetchedSeatBooking.Id);
            Assert.AreEqual(seatBooking.SeatStatus, fetchedSeatBooking.SeatStatus);
        }

        [Test]
        public void TestGetException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(999));
        }

        [Test]
        public async Task TestGetAll()
        {
            var seatBooking = CreateSeatBooking();
            await _context.SeatsBooked.AddAsync(seatBooking);
            await _context.SaveChangesAsync();

            var result = await _repository.GetAll();

            Assert.IsNotEmpty(result);
            Assert.AreEqual(result.Count(), 1);
        }

        [Test]
        public void TestGetAllException()
        {
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await _repository.GetAll());
        }

        [Test]
        public async Task TestUpdate()
        {
            var seatBooking = CreateSeatBooking();
            await _context.SeatsBooked.AddAsync(seatBooking);
            await _context.SaveChangesAsync();

            seatBooking.SeatStatus = Status.Pending; // Update seat status
            var updatedSeatBooking = await _repository.Update(seatBooking, seatBooking.Id);

            Assert.AreEqual(Status.Pending, updatedSeatBooking.SeatStatus);
            Assert.AreEqual(seatBooking.Id, updatedSeatBooking.Id);
        }

        [Test]
        public void TestUpdateException()
        {
            var seatBooking = new SeatsBooked
            {
                Id = 999,
                SeatStatus = Status.Pending
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Update(seatBooking, seatBooking.Id));
        }
    }
}
