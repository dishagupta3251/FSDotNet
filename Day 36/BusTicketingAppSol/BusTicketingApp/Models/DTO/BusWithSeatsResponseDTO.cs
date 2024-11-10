namespace BusTicketingApp.Models.DTO
{
    public class BusWithSeatsResponseDTO
    {
        public string BusNumber { get; set; } = string.Empty;
        public string BusType { get; set; }=string.Empty;
        public decimal StandardFare { get; set; }
        public decimal PremiumFare { get; set; }

        public List<SeatsResponseDTO> Seats { get; set; }=new List<SeatsResponseDTO>();
    }
}
