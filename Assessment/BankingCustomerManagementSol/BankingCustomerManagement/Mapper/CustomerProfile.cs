using AutoMapper;
using BankingCustomerManagement.Models;
using BankingCustomerManagement.Models.DTO;

namespace BankingCustomerManagement.Mapper
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Customer, CustomerDTO>();
        }
    }
}
