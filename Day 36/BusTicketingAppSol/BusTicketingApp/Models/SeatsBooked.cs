namespace BusTicketingApp.Models
{

    public enum Status
    {
        Pending,
        Confirmed
    }
    public class SeatsBooked
    {
        public int Id { get; set; }

        public int BusId { get; set; }
        public int SeatId { get; set; }
        public int CustomerId {  get; set; }
        public int BookingId {  get; set; }
        
        public Status SeatStatus { get; set; }

        public Bus Bus { get; set; }
        public Booking Booking { get; set; }

        public Customer Customer { get; set; }

        public Seats Seats { get; set; }
    }
}
