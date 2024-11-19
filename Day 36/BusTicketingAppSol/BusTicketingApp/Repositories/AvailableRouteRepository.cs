using BusTicketingApp.Contexts;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace BusTicketingApp.Repositories
{
    public class AvailableRouteRepository : IRepository<AvailableRoute, int>
    {
        private readonly TicketingContext _ticketingContext;
        private readonly ILogger<AvailableRouteRepository> _logger;

        public AvailableRouteRepository(TicketingContext ticketingContext, ILogger<AvailableRouteRepository> logger)
        {
            _ticketingContext = ticketingContext;
            _logger = logger;
        }

        public async Task<AvailableRoute> Add(AvailableRoute entity)
        {
            try
            {
                _ticketingContext.AvailableRoutes.Add(entity);
                await _ticketingContext.SaveChangesAsync();
                _logger.LogInformation("Added a new AvailableRoute with ID {RouteId}.", entity.RouteId);
                return entity;
            }
            catch
            {
                _logger.LogError("Failed to add a new AvailableRoute.");
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
                    _logger.LogInformation("Deleted AvailableRoute with ID {RouteId}.", key);
                }
                return availableRoute;
            }
            catch
            {
                _logger.LogError("Failed to delete AvailableRoute with ID {RouteId}.", key);
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
                _logger.LogInformation("Retrieved AvailableRoute with ID {RouteId}.", key);
                return availableRoute;
            }
            catch
            {
                _logger.LogError("AvailableRoute with ID {RouteId} not found.", key);
                throw new NotFoundException("AvailableRoute");
            }
        }

        public async Task<IEnumerable<AvailableRoute>> GetAll()
        {
            try
            {
                var availableRoutes = await _ticketingContext.AvailableRoutes.ToListAsync();
                if (availableRoutes.Count == 0) throw new Exception();
                _logger.LogInformation("Retrieved all AvailableRoutes. Count: {Count}.", availableRoutes.Count);
                return availableRoutes;
            }
            catch
            {
                _logger.LogError("No AvailableRoutes found.");
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

                await _ticketingContext.SaveChangesAsync();

                _logger.LogInformation("Updated AvailableRoute with ID {RouteId}.", key);
                return existingAvailableRoute;
            }
            catch
            {
                _logger.LogError("Failed to update AvailableRoute with ID {RouteId}.", key);
                throw new NotFoundException("AvailableRoute");
            }
        }
    }
}
