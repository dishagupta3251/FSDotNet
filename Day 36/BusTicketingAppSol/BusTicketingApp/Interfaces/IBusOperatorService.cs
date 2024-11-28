using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface IBusOperatorService
    {
        public Task<BusOperator> AddBusOperator(BusOperatorCreateDTO busOperatorCreateDTO);
        public Task<BusOperator> UpdateBusOperator(int id, BusOperatorCreateDTO busOperatorCreateDTO);
        public Task<BusOperator> GetBusOperatorById(int id);

        public Task<IEnumerable<Bus>> GetBusesByOperator(int userId);

        public Task<IEnumerable<Booking>> GetBookingsByBus(int busId);

        public Task<ReviewResponseDTO> GetOperatorReview(int id);
        public Task<IEnumerable<BusOperator>> GetAllBusOperators();
    }
}
