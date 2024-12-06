namespace BusTicketingApp.Models.DTO
{
    public class BusWithSeatsResponseDTO
    {
        public string BusNumber { get; set; } = string.Empty;
        public int BusId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public List<SeatsResponseDTO> Seats { get; set; }=new List<SeatsResponseDTO>();
    }
}
