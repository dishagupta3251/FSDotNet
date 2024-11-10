using AutoMapper;
using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Mapper
{
    public class PaymentProfile:Profile
    {
        public PaymentProfile()
        {
            CreateMap<Payment,PaymentRequestDTO>();
            CreateMap<PaymentRequestDTO, Payment>();
        }
    }
}
