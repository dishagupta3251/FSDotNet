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
        public RoutingService(IRepository<AvailableRoute, int> repository,IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }
        public async Task<AvailableRoute> AddNewRoutes(AvailableRouteDTO routeData)
        {
            try
            {var newRoute = _mapper.Map<AvailableRoute>(routeData);
                if (newRoute == null) throw new Exception(); 
             newRoute = await _repository.Add(newRoute);
                return newRoute; 
            }
            catch {
                throw new Exception("Route cannot be added");
            }

        }

        public async Task<IEnumerable<AvailableRouteDTO>> GetAllRoutes()
        {
            try
            {
                var routes=await _repository.GetAll();
                var routesList=_mapper.Map<IEnumerable<AvailableRouteDTO>>(routes);
                if(routes.Count()==0) throw new CollectionEmptyException("Routes");
                return routesList;
            }
            catch {
                throw new CollectionEmptyException("Routes");
            }
        }
    }
}
