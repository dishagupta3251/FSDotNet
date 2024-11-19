using System;
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
    public class PaymentRepositoryTest
    {
        private DbContextOptions<TicketingContext> _options;
        private TicketingContext _context;
        private PaymentRepository _repository;

        [SetUp]
        public void Setup()
        {
            _options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase("TestPaymentRepo")
                .Options;

            _context = new TicketingContext(_options);
            _repository = new PaymentRepository(_context);
        }

        private Payment CreatePayment()
        {
            // Adding dependent entities
            var customer = new Customer { CustomerId = 1, CustomerName = "John Doe", Email = "john.doe@example.com" };
            var route = new AvailableRoute { RouteId = 1, Origin = "Route1", Destination="Route2" };
            var bus = new Bus
            {
                BusId = 1,
                BusNumber = "B123",
                BusType = BusTypes.AC,
                NumberOfSeats = 40,
                Status = BusStatus.Running,
                StandardFare = 500,
                PremiumFare = 1000,
                RouteId = route.RouteId
            };

            var booking = new Booking
            {
                BookingId = 1,
                BookingDate = DateTime.Now,
                BookedForDate = DateTime.Now.AddDays(1),
                BookedForDay = DaysOfWeek.Monday,
                BookedSeats = "1,2,3",
                TotalFare = 1500,
                IsConfirmed = "Yes",
                CustomerId = customer.CustomerId,
                RouteId = route.RouteId,
                BusId = bus.BusId,
                BusNumber = bus.BusNumber
            };

            _context.Customers.Add(customer);
            _context.AvailableRoutes.Add(route);
            _context.Buses.Add(bus);
            _context.Bookings.Add(booking);
            _context.SaveChanges();

            return new Payment
            {
                Id = 1,
                DateTime = DateTime.Now,
                BookingId = booking.BookingId,
                Type = PaymentTypes.Credit_card
            };
        }

        [Test]
        public async Task TestAdd()
        {
            var payment = CreatePayment();
            var result = await _repository.Add(payment);

            Assert.AreEqual(payment.Id, result.Id);
            Assert.AreEqual(payment.Type, result.Type);
        }

        [Test]
        public void TestAddException()
        {
            Assert.ThrowsAsync<CouldNotAddException>(async () => await _repository.Add(null));
        }

        [Test]
        public async Task TestDelete()
        {
            var payment = CreatePayment();
            var addedPayment = await _repository.Add(payment);

            var deletedPayment = await _repository.Delete(addedPayment.Id);

            Assert.AreEqual(deletedPayment.Id, addedPayment.Id);
        }

        [Test]
        public void TestDeleteException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Delete(0));
        }

        [Test]
        public async Task TestGet()
        {
            var payment = CreatePayment();
            var addedPayment = await _repository.Add(payment);

            var fetchedPayment = await _repository.Get(addedPayment.Id);

            Assert.AreEqual(fetchedPayment.Id, addedPayment.Id);
            Assert.AreEqual(fetchedPayment.Type, addedPayment.Type);
        }

        [Test]
        public void TestGetException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Get(999));
        }

        [Test]
        public async Task TestGetAll()
        {
            _context.Payments.RemoveRange(_context.Payments); // Ensure no data in the context.
            await _context.SaveChangesAsync();

            var payment = CreatePayment();
            await _repository.Add(payment);

            var payments = await _repository.GetAll();

            Assert.IsNotEmpty(payments);
            Assert.AreEqual(payments.Count(), 1);
        }

        [Test]
        public void TestGetAllException()
        {
            _context.Payments.RemoveRange(_context.Payments); // Ensure no data in the context.
            _context.SaveChanges();
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await _repository.GetAll());
        }

        [Test]
        public async Task TestUpdate()
        {
            var payment = CreatePayment();
            var addedPayment = await _repository.Add(payment);

            addedPayment.Type = PaymentTypes.UPI;

            var updatedPayment = await _repository.Update(addedPayment, addedPayment.Id);

            Assert.AreEqual(updatedPayment.Type, PaymentTypes.UPI);
        }

        [Test]
        public void TestUpdateException()
        {
            var invalidPayment = new Payment
            {
                Id = 999,
                Type = PaymentTypes.Wallet
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await _repository.Update(invalidPayment, invalidPayment.Id));
        }
    }
}
