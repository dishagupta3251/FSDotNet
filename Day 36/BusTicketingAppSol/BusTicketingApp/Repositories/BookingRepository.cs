using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketingApp.Repositories
{
    public class BookingRepository : IRepository<Booking, int>
    {
        private readonly TicketingContext _ticketingContext;

        public BookingRepository(TicketingContext ticketingContext)
        {
            _ticketingContext = ticketingContext;
        }

        public async Task<Booking> Add(Booking entity)
        {
            try
            {
                _ticketingContext.Bookings.Add(entity);
                await _ticketingContext.SaveChangesAsync();
                return entity;
            }
            catch
            {
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
                }
                return bookingEntity;
            }
            catch
            {
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
                return bookingEntity;
            }
            catch
            {
                throw new NotFoundException("Booking");
            }
        }

        public async Task<IEnumerable<Booking>> GetAll()
        {
            try
            {
                var bookings = await _ticketingContext.Bookings.ToListAsync();

                if (bookings.Count == 0) throw new Exception();
                return bookings;
            }
            catch
            {
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
                existingBooking.SeatsBooked = entity.SeatsBooked;
                existingBooking.TotalFare = entity.TotalFare;
                existingBooking.IsConfirmed = entity.IsConfirmed;
                existingBooking.CustomerId = entity.CustomerId;
                existingBooking.RouteId = entity.RouteId;
                existingBooking.SeatId = entity.SeatId;
               
                existingBooking.Payment = entity.Payment ?? existingBooking.Payment;

                _ticketingContext.Bookings.Update(existingBooking);
                await _ticketingContext.SaveChangesAsync();

                return existingBooking;
            }
            catch
            {
                throw new NotFoundException("Booking");
            }
        }
    }
}
