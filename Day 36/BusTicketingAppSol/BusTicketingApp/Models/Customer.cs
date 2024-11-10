namespace BusTicketingApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public int Age {  get; set; }

        public string City { get; set; }
        public int UserId { get; set; } 
        
        public IEnumerable<Booking> Bookings { get; set; }

        public IEnumerable<Seats> Seats { get; set; }
        public User User { get; set; }   
       
    }
}
