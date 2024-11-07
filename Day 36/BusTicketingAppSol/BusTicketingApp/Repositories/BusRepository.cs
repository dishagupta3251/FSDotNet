using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketingApp.Repositories
{
    public class BusRepository:IRepository<Bus,int>
    {
        private readonly TicketingContext _ticketingContext;
        public BusRepository(TicketingContext ticketingContext)
        {
            _ticketingContext = ticketingContext;
        }

        public async Task<Bus> Add(Bus entity)
        {
            
            try
            {
                int halfSeats = entity.NumberOfSeats / 2;
                _ticketingContext.Buses.Add(entity);
                for(int i=1;i<=entity.NumberOfSeats;i++)
                {
                    var seat = new Seats()
                    {
                           SeatNumber = i,
                           Side=i<=halfSeats?"L":"R",
                           SeatType=i%2==0?"A":"W",
                           IsBooked=false,
                           Price= i % 2 == 0?  entity.StandardFare:entity.PremiumFare,
                           BusId=entity.BusId,


                    };
                    _ticketingContext.Seats.Add(seat); //Creating seats based on busId
                    await _ticketingContext.SaveChangesAsync();
                }
                await _ticketingContext.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new CouldNotAddException("User");
            }
        }

        public async Task<Bus> Delete(int key)
        {
            try
            {
                var bus = await Get(key);
                if (bus != null)
                {
                    _ticketingContext.Buses.Remove(bus);
                    await _ticketingContext.SaveChangesAsync();

                }
                return bus;
            }
            catch { throw new NotFoundException("Bus"); }
        }

        public async Task<Bus> Get(int key)
        {
            try
            {
                var bus= await _ticketingContext.Buses.FirstOrDefaultAsync(u => u.BusId == key);
                if (bus == null) throw new Exception();
                return bus;
            }
            catch
            {
                throw new NotFoundException("Bus");
            }
        }

        public async Task<IEnumerable<Bus>> GetAll()
        {
            try
            {
                var buses = await _ticketingContext.Buses.ToListAsync();
                if (buses.Count == 0) throw new Exception();
                return buses;
            }
            catch
            {
                throw new CollectionEmptyException("Bus");
            }
        }

        public async Task<Bus> Update(Bus entity, int key)
        {
            var exsistingBus = await Get(key);
            exsistingBus.BusName = entity.BusName??exsistingBus.BusName;
            exsistingBus.BusType = entity.BusType==exsistingBus.BusType?exsistingBus.BusType:entity.BusType;
           
            exsistingBus.Status = entity.Status ?? exsistingBus.Status;
            exsistingBus.StandardFare = entity.StandardFare == 0 ? exsistingBus.StandardFare : entity.StandardFare;
            exsistingBus.PremiumFare = entity.PremiumFare == 0 ? exsistingBus.PremiumFare : entity.PremiumFare;

            _ticketingContext.Buses.Update(exsistingBus);
            await _ticketingContext.SaveChangesAsync();

            return exsistingBus;

        }
    }
}
