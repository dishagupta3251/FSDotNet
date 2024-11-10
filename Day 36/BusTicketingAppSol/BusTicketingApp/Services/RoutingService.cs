using AutoMapper;
using BusTicketingApp.Exceptions;
using BusTicketingApp.Interfaces;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Services
{
    public class RoutingService : IRoutingService
    {
        private readonly IRepository<AvailableRoute,int> _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<RoutingService> _logger;

        public RoutingService(IRepository<AvailableRoute, int> repository,IMapper mapper, ILogger<RoutingService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        public async Task<int> AddNewRoutes(AvailableRouteDTO routeData)
        {
            try
            {
                
                var newRoute = _mapper.Map<AvailableRoute>(routeData);
                if (newRoute == null) throw new Exception();
                _logger.LogInformation("Adding routes information");
                var  finalRoute = await _repository.Add(newRoute);
                return finalRoute.RouteId; 
            }
            catch {
                throw new Exception("Route cannot be added");
            }

        }

        public async Task<IEnumerable<AvailableRoute>> GetAllRoutes()
        {
            try
            {
                _logger.LogInformation("Getting information....");
                var routes=await _repository.GetAll();
               
                if(routes.Count()==0) throw new CollectionEmptyException("Routes");
                return routes;
            }
            catch {
                _logger.LogError("Your route collection is empty");
                throw new CollectionEmptyException("Routes");
            }
        }

        public async Task<int> GetIdByJourney(string checkSource, string checkDestination)
        {
            try
            {
                var route=(await _repository.GetAll()).FirstOrDefault(r=>r.Origin==checkSource && r.Destination==checkDestination);
                return route.RouteId;

            }
            catch
            {
                throw new Exception("Cannot get id by journey details");
            }
        }
    }
}
