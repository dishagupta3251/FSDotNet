using AutoMapper;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Mapper
{
    public class RouteMapper:Profile
    {
        public RouteMapper()
        {
            CreateMap<AvailableRoute, AvailableRouteDTO>();
            CreateMap<AvailableRouteDTO, AvailableRoute>();
        }
    }
}
