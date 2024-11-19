using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;


namespace BusTicketingApp.Repositories
{
    public class BusScheduleRepository : IRepository<BusSchedule, int>
    {
        private readonly TicketingContext _ticketingContext;

        public BusScheduleRepository(TicketingContext ticketingContext)
        {
            _ticketingContext = ticketingContext;
        }

        public async Task<BusSchedule> Add(BusSchedule entity)
        {
            try
            {
                _ticketingContext.BusSchedules.Add(entity);
                await _ticketingContext.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new CouldNotAddException("BusSchedule");
            }
        }

        public async Task<BusSchedule> Delete(int key)
        {
            try
            {
                var scheduleEntity = await Get(key);
                if (scheduleEntity != null)
                {
                    _ticketingContext.BusSchedules.Remove(scheduleEntity);
                    await _ticketingContext.SaveChangesAsync();
                }
                return scheduleEntity;
            }
            catch
            {
                throw new NotFoundException("BusSchedule");
            }
        }

        public async Task<BusSchedule> Get(int key)
        {
            try
            {
                var scheduleEntity = await _ticketingContext.BusSchedules
                    .Include(bs => bs.Bus)
                    .Include(bs => bs.AvailableRoute)
                    .FirstOrDefaultAsync(bs => bs.BusScheduleId == key);

                if (scheduleEntity == null) throw new Exception();
                return scheduleEntity;
            }
            catch
            {
                throw new NotFoundException("BusSchedule");
            }
        }

        public async Task<IEnumerable<BusSchedule>> GetAll()
        {
            try
            {
                var schedules = await _ticketingContext.BusSchedules
                    .Include(bs => bs.Bus)
                    .Include(bs => bs.AvailableRoute)
                    .ToListAsync();

                if (schedules.Count == 0) throw new Exception();
                return schedules;
            }
            catch
            {
                throw new CollectionEmptyException("BusSchedules");
            }
        }

        public async Task<BusSchedule> Update(BusSchedule entity, int key)
        {
            try
            {
                var existingSchedule = await Get(key);
                existingSchedule.Day = entity.Day;
                existingSchedule.RouteId = entity.RouteId;
                existingSchedule.BusId = entity.BusId;

                
                await _ticketingContext.SaveChangesAsync();

                return existingSchedule;
            }
            catch
            {
                throw new NotFoundException("BusSchedule");
            }
        }
    }
}
