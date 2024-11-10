using BusTicketingApp.Models;
using BusTicketingApp.Models.DTO;

namespace BusTicketingApp.Interfaces
{
    public interface IBookingService
    {
        public Task<IEnumerable<BusResponseDTO>> GetAllBusesOnRoute(string from, string to, DateTime dateTime
            );

        public Task<BusWithSeatsResponseDTO> ViewBusDetailsAlongWithSeats(int busId);

        public Task<IEnumerable<SeatsResponseDTO>> SelectSeats(SeatSelectionRequestDTO seatSelection);

        public Task<string> PaymentIntiation(PaymentRequestDTO paymentRequestDTO);
        public Task<BookingHistoryDTO> BookingHistory(int customerId);

        public Task<BookingResponseDTO> BookingConfirmation();
        
    }
}
