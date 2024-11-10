using AutoMapper;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Mapper
{
    public class BusOperatorProfile:Profile
    {
        public BusOperatorProfile()
        {
            CreateMap<BusOperatorCreateDTO, BusOperator>();
            CreateMap<BusOperator,BusOperatorCreateDTO>();
        }
    }
}
