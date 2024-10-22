using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Contexts;
using WebApplication1.Interfaces;
using WebApplication1.Models;

namespace WebApplication1.Repositories
{
    public class EventRepository : IRepository<int, Event>
    {
        private readonly EventBookingContext _eventBookingContext;
        public EventRepository(EventBookingContext eventBookingContext)
        {
            _eventBookingContext = eventBookingContext;
        }

        public async Task<Event> Add(Event entity)
        {
            try
            {
                _eventBookingContext.Events.Add(entity);
                await _eventBookingContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public async Task<Event> Delete(int key)
        {
            try
            {
                var events = await Get(key);
                _eventBookingContext.Events.Remove(events);
                await _eventBookingContext.SaveChangesAsync();
                return events;


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<Event> Get(int key)
        {
            return  await _eventBookingContext.Events.FirstOrDefaultAsync(e=>e.EventId == key);
        }

        public async Task<IEnumerable<Event>> GetAll()
        {
            var events = await _eventBookingContext.Events.ToListAsync();
            if (events.Count < 0)
            {
                Console.WriteLine("No event found");
            }
            return events;
        }

        public async Task<Event> Update(Event entity)
        {
            var oldEvent = await Get(entity.EventId);
            if (oldEvent != null)
            {
                oldEvent.Name = entity.Name;
                oldEvent.Time = entity.Time;
                oldEvent.Type = entity.Type;
               oldEvent.Description = entity.Description;
                await _eventBookingContext.SaveChangesAsync();
            }
            else
            {
                Console.WriteLine("No event found");
            }
            return entity;
        }
    }
}