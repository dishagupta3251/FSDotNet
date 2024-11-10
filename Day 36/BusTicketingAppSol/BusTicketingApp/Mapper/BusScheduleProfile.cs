using AutoMapper;
using BusTicketingApp.Models;

namespace BusTicketingApp.Mapper
{
    public class BusScheduleProfile:Profile
    {
        public BusScheduleProfile()
        {
            CreateMap<BusSchedule, Bus>();
            CreateMap<Bus, BusSchedule>();
        }
    }
}
