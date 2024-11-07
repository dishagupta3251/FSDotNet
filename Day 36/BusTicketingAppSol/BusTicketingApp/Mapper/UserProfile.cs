using AutoMapper;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Mapper
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<UserProfileDTO, User>();
            CreateMap<User, UserProfileDTO>();
        }
    }
}
