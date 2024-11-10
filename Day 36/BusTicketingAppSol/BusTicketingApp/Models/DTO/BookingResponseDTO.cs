namespace BusTicketingApp.Models.DTO
{
    public class BookingResponseDTO
    {
        public int BookingId { get; set; }
        public int CustomerId {  get; set; }
        public DateTime BookingDate { get; set; }
        public DaysOfWeek BookedForDay { get; set; }
        public int SeatsBooked { get; set; }
        public decimal TotalFare { get; set; }
    }
}