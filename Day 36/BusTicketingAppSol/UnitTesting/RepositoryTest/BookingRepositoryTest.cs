using BusTicketingApp.Contexts;
using BusTicketingApp.Repositories;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace UnitTesting.RepositoryTest
{
    public class BookingRepositoryTest
    {
        DbContextOptions options;
        TicketingContext context;
        BookingRepository repository;
        ILogger<BookingRepository> logger;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<TicketingContext>()
                .UseInMemoryDatabase("TestBookingRepo")
                .Options;

            context = new TicketingContext(options);
            logger = new LoggerFactory().CreateLogger<BookingRepository>(); // Use a real logger
            repository = new BookingRepository(context, logger);
        }

        private Booking AddBookingDetails()
        {
            return new Booking
            {
                BookingDate = System.DateTime.Now,
                BusNumber = "AB123",
                BookedForDate = System.DateTime.Now.AddDays(1),
                BookedForDay = DaysOfWeek.Monday,
                BookedSeats = "1A,1B",
                TotalFare = 150.75m,
                IsConfirmed = "Yes",
                CustomerId = 1,
                RouteId = 101,
                BusId = 201,
                Payment = new Payment(), // Assuming Payment model is already set up.
                SeatsBooked = new List<SeatsBooked> { new SeatsBooked() }
            };
        }

        [Test]
        public async Task TestAdd()
        {
            var booking = AddBookingDetails();
            var result = await repository.Add(booking);

            Assert.AreEqual(result.BookingId, booking.BookingId);
            Assert.AreEqual(result.BusNumber, booking.BusNumber);
        }

        [Test]
        public void TestAddException()
        {
            Assert.ThrowsAsync<CouldNotAddException>(async () => await repository.Add(null));
        }

        [Test]
        public async Task TestDelete()
        {
            var booking = AddBookingDetails();
            var addedBooking = await repository.Add(booking);

            var deletedBooking = await repository.Delete(addedBooking.BookingId);
            Assert.AreEqual(deletedBooking.BookingId, addedBooking.BookingId);
        }

        [Test]
        public void TestDeleteException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Delete(999)); // non-existent booking ID
        }

        [Test]
        public async Task TestGet()
        {
            var booking = AddBookingDetails();
            var addedBooking = await repository.Add(booking);

            var fetchedBooking = await repository.Get(addedBooking.BookingId);
            Assert.AreEqual(fetchedBooking.BookingId, addedBooking.BookingId);
        }

        [Test]
        public void TestGetException()
        {
            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Get(999)); // non-existent booking ID
        }

        [Test]
        public async Task GetAllTest()
        {
            context.Bookings.RemoveRange(context.Bookings); 
            var booking = AddBookingDetails();
            await repository.Add(booking);

            var bookings = await repository.GetAll();
            Assert.IsNotEmpty(bookings);
        }

        [Test]
        public void GetAllTestException()
        {
            context.Bookings.RemoveRange(context.Bookings); // Ensure no data in the context.
            Assert.ThrowsAsync<CollectionEmptyException>(async () => await repository.GetAll());
        }

        [Test]
        public async Task UpdateTest()
        {
            var booking = AddBookingDetails();
            var addedBooking = await repository.Add(booking);

            booking.BusNumber = "UpdatedBusNumber";
            var updatedBooking = await repository.Update(booking, addedBooking.BookingId);

            Assert.AreEqual(updatedBooking.BusNumber, "UpdatedBusNumber");
        }

        [Test]
        public void UpdateTestException()
        {
            var invalidBooking = new Booking
            {
                BookingId = 999, // non-existent booking ID
                BusNumber = "NonExistent"
            };

            Assert.ThrowsAsync<NotFoundException>(async () => await repository.Update(invalidBooking, 999));
        }
    }
}
