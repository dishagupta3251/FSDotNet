using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusTicketingApp.Repositories
{
    public class SeatRepository : IRepository<Seats, int>
    {
        private readonly TicketingContext _ticketingContext;

        public SeatRepository(TicketingContext ticketingContext)
        {
            _ticketingContext = ticketingContext;
        }

        public async Task<Seats> Add(Seats entity)
        {
            try
            {
                _ticketingContext.Seats.Add(entity);
                await _ticketingContext.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new CouldNotAddException("Seat");
            }
        }

        public async Task<Seats> Delete(int key)
        {
            try
            {
                var seatEntity = await Get(key);
                if (seatEntity != null)
                {
                    _ticketingContext.Seats.Remove(seatEntity);
                    await _ticketingContext.SaveChangesAsync();
                }
                return seatEntity;
            }
            catch
            {
                throw new NotFoundException("Seat");
            }
        }

        public async Task<Seats> Get(int key)
        {
            try
            {
                var seatEntity = await _ticketingContext.Seats.FirstOrDefaultAsync(s => s.SeatsId == key);
                if (seatEntity == null) throw new Exception();
                return seatEntity;
            }
            catch
            {
                throw new NotFoundException("Seat");
            }
        }

        public async Task<IEnumerable<Seats>> GetAll()
        {
            try
            {
                var seats = await _ticketingContext.Seats.ToListAsync();
                if (seats.Count == 0) throw new Exception();
                return seats;
            }
            catch
            {
                throw new CollectionEmptyException("Seats");
            }
        }

        public async Task<Seats> Update(Seats entity, int key)
        {
            try
            {
                var existingSeat = await Get(key);
                existingSeat.IsBooked = entity.IsBooked;
                existingSeat.SeatNumber = entity.SeatNumber;

                _ticketingContext.Seats.Update(existingSeat);
                await _ticketingContext.SaveChangesAsync();

                return existingSeat;
            }
            catch
            {
                throw new NotFoundException("Seat");
            }
        }
    }
}
