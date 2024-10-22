using WebApplication1.Models.DTO;
using WebApplication1.Models;
using AutoMapper;

namespace WebApplication1.Mapper
{
    public class BookingProfile:Profile
    {
        public BookingProfile()
        {
            CreateMap<Booking, BookingDTO>();
            CreateMap<BookingDTO, Booking>();
        }
    }
}
