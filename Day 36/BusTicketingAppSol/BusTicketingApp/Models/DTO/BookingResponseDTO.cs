namespace BusTicketingApp.Models.DTO
{
    public class BookingResponseDTO
    {
        public int BookingId { get; set; }
        public DateTime BookingDate { get; set; } = DateTime.Now;
        public string BusNumber { get; set; }

        public string Destination { get; set; } = string.Empty;
        public string Source {  get; set; } = string.Empty;
        public int CustomerId {  get; set; }
        public DateTime BookedForDate { get; set; }
        public DaysOfWeek BookedForDay { get; set; }
        public IEnumerable<SeatsResponseDTO> SeatsBooked { get; set; }=new List<SeatsResponseDTO>();
        public decimal TotalFare { get; set; }
        public string IsConfirmed { get; set; }=string.Empty;
    }
}