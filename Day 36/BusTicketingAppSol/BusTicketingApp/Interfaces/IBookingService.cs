using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface IBookingService
    {
        public Task<IEnumerable<BusResponseDTO>> GetAllBusesOnRoute(string from, string to, DateTime dateTime
            );

        public Task<BusWithSeatsResponseDTO> ViewBusDetailsAlongWithSeats(int busId);


        public Task<BookingResponseDTO> PaymentIntiation(PaymentRequestDTO paymentRequestDTO);
        public Task<BookingHistoryDTO> BookingHistory(int customerId);

        public Task<int> BookingConfirmation(SeatSelectionRequestDTO seatSelectionRequestDTO,DateTime date);
        
    }
}
