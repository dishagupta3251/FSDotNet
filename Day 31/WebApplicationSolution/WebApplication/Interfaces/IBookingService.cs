using WebApplication1.Models.DTO;
using WebApplication1.Models;

namespace WebApplication1.Interfaces
{
    public interface IBookingService
    {

        public Task<Booking> Add(BookingDTO bookingDTO);
        public Task<IEnumerable<Booking>> GetAll();
    }
}
