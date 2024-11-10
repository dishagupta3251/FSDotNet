using BusTicketingApp.Models;

namespace BusTicketingApp.Interfaces
{
    public interface ISeatService
    {
        public Task<IEnumerable<Seats>> GetAllSeats();
        public Task<IEnumerable<string>> UpdateSeatStatus(List<int> seatIds);
    }
}
