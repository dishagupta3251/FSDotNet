namespace BusTicketingApp.Models.DTO
{
    public class SeatsResponseDTO
    {
 
        public int SeatId { get; set; }
        public string Seat { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
