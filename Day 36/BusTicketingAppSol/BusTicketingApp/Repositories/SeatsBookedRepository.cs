using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketingApp.Repositories
{
    public class SeatsBookedRepository : IRepository<SeatsBooked, int>
    {
        private readonly TicketingContext _ticketingContext;

        public SeatsBookedRepository(TicketingContext ticketingContext)
        {
            _ticketingContext = ticketingContext;
        }

        public async Task<SeatsBooked> Add(SeatsBooked entity)
        {
            try
            {
                _ticketingContext.SeatsBooked.Add(entity);
                await _ticketingContext.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new CouldNotAddException("SeatsBooked");
            }
        }

        public async Task<SeatsBooked> Delete(int key)
        {
            try
            {
                var seatBooking = await Get(key);
                if (seatBooking != null)
                {
                    _ticketingContext.SeatsBooked.Remove(seatBooking);
                    await _ticketingContext.SaveChangesAsync();
                }
                return seatBooking;
            }
            catch
            {
                throw new NotFoundException("SeatsBooked");
            }
        }

        public async Task<SeatsBooked> Get(int key)
        {
            try
            {
                var seatBooking = await _ticketingContext.SeatsBooked
                    .FirstOrDefaultAsync(sb => sb.Id == key);

                if (seatBooking == null) throw new Exception();
                return seatBooking;
            }
            catch
            {
                throw new NotFoundException("SeatsBooked");
            }
        }

        public async Task<IEnumerable<SeatsBooked>> GetAll()
        {
            try
            {
                var seatBookings = await _ticketingContext.SeatsBooked
                    .ToListAsync();

                
                return seatBookings;
            }
            catch
            {
                throw new CollectionEmptyException("SeatsBooked");
            }
        }

        public async Task<SeatsBooked> Update(SeatsBooked entity, int key)
        {
            try
            {
                var existingSeatBooking = await Get(key);

                existingSeatBooking.BusId = entity.BusId != 0 ? entity.BusId : existingSeatBooking.BusId;
                existingSeatBooking.SeatId = entity.SeatId != 0 ? entity.SeatId : existingSeatBooking.SeatId;
                existingSeatBooking.CustomerId = entity.CustomerId != 0 ? entity.CustomerId : existingSeatBooking.CustomerId;
                existingSeatBooking.BookingId = entity.BookingId != 0 ? entity.BookingId : existingSeatBooking.BookingId;
                existingSeatBooking.SeatStatus= entity.SeatStatus;

              
                await _ticketingContext.SaveChangesAsync();

                return existingSeatBooking;
            }
            catch
            {
                throw new NotFoundException("SeatsBooked");
            }
        }

    }
}
