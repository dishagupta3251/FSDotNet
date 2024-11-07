using AutoMapper;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;
using Microsoft.AspNetCore.Routing.Template;

namespace BusTicketingApp.Mapper
{
    public class RoutingProfiles:Profile
    {
        public RoutingProfiles()
        {
            CreateMap<AvailableRoute, AvailableRouteDTO>();
            CreateMap<AvailableRouteDTO, AvailableRoute>();
        }
    }
}
