using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;

namespace BusTicketingApp.Repositories
{
    public class AvailableRouteRepository : IRepository<AvailableRoute, int>
    {
        private readonly TicketingContext _ticketingContext;

        public AvailableRouteRepository(TicketingContext ticketingContext)
        {
            _ticketingContext = ticketingContext;
        }

        public async Task<AvailableRoute> Add(AvailableRoute entity)
        {
            try
            {
                _ticketingContext.AvailableRoutes.Add(entity);
                await _ticketingContext.SaveChangesAsync();
                return entity;
            }
            catch
            {
                throw new CouldNotAddException("AvailableRoute");
            }
        }

        public async Task<AvailableRoute> Delete(int key)
        {
            try
            {
                var availableRoute = await Get(key);
                if (availableRoute != null)
                {
                    _ticketingContext.AvailableRoutes.Remove(availableRoute);
                    await _ticketingContext.SaveChangesAsync();
                }
                return availableRoute;
            }
            catch
            {
                throw new NotFoundException("AvailableRoute");
            }
        }

        public async Task<AvailableRoute> Get(int key)
        {
            try
            {
                var availableRoute = await _ticketingContext.AvailableRoutes
                    .FirstOrDefaultAsync(ar => ar.RouteId == key);
                if (availableRoute == null) throw new Exception();
                return availableRoute;
            }
            catch
            {
                throw new NotFoundException("AvailableRoute");
            }
        }

        public async Task<IEnumerable<AvailableRoute>> GetAll()
        {
            try
            {
                var availableRoutes = await _ticketingContext.AvailableRoutes.ToListAsync();
                if (availableRoutes.Count == 0) throw new Exception();
                return availableRoutes;
            }
            catch
            {
                throw new CollectionEmptyException("AvailableRoutes");
            }
        }

        public async Task<AvailableRoute> Update(AvailableRoute entity, int key)
        {
            try
            {
                var existingAvailableRoute = await Get(key);
                existingAvailableRoute.Origin = entity.Origin ?? existingAvailableRoute.Origin;
                existingAvailableRoute.Destination = entity.Destination ?? existingAvailableRoute.Destination;

                _ticketingContext.AvailableRoutes.Update(existingAvailableRoute);
                await _ticketingContext.SaveChangesAsync();

                return existingAvailableRoute;
            }
            catch
            {
                throw new NotFoundException("AvailableRoute");
            }
        }
    }
}
