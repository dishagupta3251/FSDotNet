using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface IRoutingService
    {
        public Task<AvailableRoute> AddNewRoutes(AvailableRouteDTO routeData);
        public Task<IEnumerable<AvailableRouteDTO>> GetAllRoutes();

    }
}
