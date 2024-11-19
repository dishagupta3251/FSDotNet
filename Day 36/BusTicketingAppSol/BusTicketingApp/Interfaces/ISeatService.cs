using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface ISeatService
    {
        public Task<IEnumerable<Seats>> GetAllSeats();

        public Task<Seats> SeatById(int id);

        public Task<SeatsResponseDTO> UpdateSeatStatus(int seatId);
    }
}
