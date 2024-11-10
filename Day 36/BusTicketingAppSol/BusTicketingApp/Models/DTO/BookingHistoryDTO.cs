namespace BusTicketingApp.Models.DTO
{
    public class BookingHistoryDTO
    {
        public List<Booking> PastBookings { get; set; }=new List<Booking>();
        public List<Booking> UpcomingBookings { get; set; }=new List<Booking>(){ };
    }
}
