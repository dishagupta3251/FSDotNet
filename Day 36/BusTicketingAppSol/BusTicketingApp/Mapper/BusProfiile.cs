using AutoMapper;
using BusTicketingApp.Models.DTO;
using BusTicketingApp.Models;
namespace BusTicketingApp.Mapper
{
    public class BusProfile:Profile
    {
        public BusProfile()
        {
            CreateMap<Bus, BusCreateDTO>();
            CreateMap<BusCreateDTO, Bus>();
        }
    }
}
