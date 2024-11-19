using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusTicketingApp.Repositories
{
    public class BookingRepository : IRepository<Booking, int>
    {
        private readonly TicketingContext _ticketingContext;
        private readonly ILogger<BookingRepository> _logger;

        public BookingRepository(TicketingContext ticketingContext, ILogger<BookingRepository> logger)
        {
            _ticketingContext = ticketingContext;
            _logger = logger;
        }

        public async Task<Booking> Add(Booking entity)
        {
            try
            {
                _ticketingContext.Bookings.Add(entity);
                await _ticketingContext.SaveChangesAsync();
                _logger.LogInformation("Added a new Booking with ID {BookingId}.", entity.BookingId);
                return entity;
            }
            catch
            {
                _logger.LogError("Failed to add a new Booking.");
                throw new CouldNotAddException("Booking");
            }
        }

        public async Task<Booking> Delete(int key)
        {
            try
            {
                var bookingEntity = await Get(key);
                if (bookingEntity != null)
                {
                    _ticketingContext.Bookings.Remove(bookingEntity);
                    await _ticketingContext.SaveChangesAsync();
                    _logger.LogInformation("Deleted Booking with ID {BookingId}.", key);
                }
                return bookingEntity;
            }
            catch
            {
                _logger.LogError("Failed to delete Booking with ID {BookingId}.", key);
                throw new NotFoundException("Booking");
            }
        }

        public async Task<Booking> Get(int key)
        {
            try
            {
                var bookingEntity = await _ticketingContext.Bookings
                    .FirstOrDefaultAsync(b => b.BookingId == key);

                if (bookingEntity == null) throw new Exception();
                _logger.LogInformation("Retrieved Booking with ID {BookingId}.", key);
                return bookingEntity;
            }
            catch
            {
                _logger.LogError("Booking with ID {BookingId} not found.", key);
                throw new NotFoundException("Booking");
            }
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            try
            {
                var bookings = await _ticketingContext.Bookings.ToListAsync();

                if (bookings.Count == 0) throw new Exception();
                _logger.LogInformation("Retrieved all Bookings. Count: {Count}.", bookings.Count);
                return bookings;
            }
            catch
            {
                _logger.LogError("No Bookings found.");
                throw new CollectionEmptyException("Bookings");
            }
        }

        public async Task<Booking> Update(Booking entity, int key)
        {
            try
            {
                var existingBooking = await Get(key);

                existingBooking.BookingDate = entity.BookingDate;
                existingBooking.BookedForDay = entity.BookedForDay;
                existingBooking.BusNumber = entity.BusNumber;
                existingBooking.SeatsBooked = entity.SeatsBooked;
                existingBooking.TotalFare = entity.TotalFare;
                existingBooking.IsConfirmed = entity.IsConfirmed;
                existingBooking.CustomerId = entity.CustomerId;
                existingBooking.RouteId = entity.RouteId;
                existingBooking.Payment = entity.Payment ?? existingBooking.Payment;

              
                await _ticketingContext.SaveChangesAsync();

                _logger.LogInformation("Updated Booking with ID {BookingId}.", key);
                return existingBooking;
            }
            catch
            {
                _logger.LogError("Failed to update Booking with ID {BookingId}.", key);
                throw new NotFoundException("Booking");
            }
        }
    }
}
