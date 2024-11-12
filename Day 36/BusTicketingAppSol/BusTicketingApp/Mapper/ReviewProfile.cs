using AutoMapper;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Mapper
{
    public class ReviewProfile:Profile
    {
        public ReviewProfile()
        {
            CreateMap<Review,ReviewRequestDTO>();
            CreateMap<ReviewRequestDTO, Review>();
        }
    }

}
