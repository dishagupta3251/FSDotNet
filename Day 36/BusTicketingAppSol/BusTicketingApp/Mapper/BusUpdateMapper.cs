using AutoMapper;
using BusTicketingApp.Models;

namespace BusTicketingApp.Mapper
{
    public class BusUpdateMapper:Profile
    {

        public BusUpdateMapper()
        {
            CreateMap<BusUpdateMapper, Bus>();
            CreateMap<Bus,BusUpdateMapper>();
        }
    }
}

