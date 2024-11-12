namespace BusTicketingApp.Models.DTO
{
    public class SeatSelectionRequestDTO
    {
        public int BusId { get; set; }
        public int CustomerId { get; set; }
        public List<int> SelectedSeatIds { get; set; }
    }
}