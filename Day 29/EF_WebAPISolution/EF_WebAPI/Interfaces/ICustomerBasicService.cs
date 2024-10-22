
using EF_WebAPI.Models.DTO;
namespace EFCoreFirstAPI.Interfaces
{
    public interface ICustomerBasicService
    {
        Task<int> CreateCustomer(CustomerDTO customer);
    }
}