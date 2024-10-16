using EF_WebAPI.Models.DTO;

namespace EF_WebAPI.Interfaces
{
     

        public interface ICustomerBasicService
        {
            Task<int> CreateCustomer(CustomerDTO customer);
        }

    
}
