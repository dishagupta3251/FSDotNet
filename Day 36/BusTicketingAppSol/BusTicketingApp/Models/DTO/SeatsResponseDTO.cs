namespace BusTicketingApp.Models.DTO
{
    public class SeatsResponseDTO
    {
 
        public string Seat { get; set; } = string.Empty;
        public bool IsBooked { get; set; }
        public decimal Price { get; set; }
    }
}
