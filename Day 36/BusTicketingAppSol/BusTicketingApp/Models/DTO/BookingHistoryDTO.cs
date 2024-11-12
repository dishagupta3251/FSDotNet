using System.Collections.Generic;

namespace BusTicketingApp.Models.DTO
{
    public class BookingHistoryDTO
    {
        public IEnumerable<Booking> PastBookings { get; set; }=new List<Booking>();
        public IEnumerable<Booking> UpcomingBookings { get; set; }=new List<Booking>(){ };
    }
}
