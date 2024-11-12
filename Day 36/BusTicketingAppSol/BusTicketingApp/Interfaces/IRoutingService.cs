using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface IRoutingService
    {
        public Task<int> AddNewRoutes(AvailableRouteDTO routeData);

        public Task<int> GetIdByJourney(string checkSource, string checkDestination);

        public Task<AvailableRoute> GetRoute(int routeId);
        public Task<IEnumerable<AvailableRoute>> GetAllRoutes();

    }
}
