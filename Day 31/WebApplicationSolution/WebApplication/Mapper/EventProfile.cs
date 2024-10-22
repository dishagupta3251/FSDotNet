using AutoMapper;
using WebApplication1.Models.DTO;
using WebApplication1.Models;

namespace WebApplication1.Mapper
{
    public class EventProfile:Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDTO>();
            CreateMap<EventDTO, Event>();
        }
    }
}
