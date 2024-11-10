using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface IBusOperatorService
    {
        Task<BusOperator> AddBusOperator(BusOperatorCreateDTO busOperatorCreateDTO);
        Task<BusOperator> UpdateBusOperator(int id, BusOperatorCreateDTO busOperatorCreateDTO);
        Task<BusOperator> GetBusOperatorById(int id);
        Task<IEnumerable<BusOperator>> GetAllBusOperators();
    }
}
