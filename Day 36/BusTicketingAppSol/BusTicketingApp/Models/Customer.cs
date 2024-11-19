namespace BusTicketingApp.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; } = string.Empty;
        public int Age { get; set; } = 0;
        public string City { get; set; } = string.Empty;
        
        public string Contact { get; set; }= string.Empty;

        public string Email { get; set; } = string.Empty;
        public int UserId { get; set; } 


        
        public IEnumerable<Booking> Bookings { get; set; }

       public IEnumerable<SeatsBooked> Seats { get; set; }
        public User User { get; set; }   
       
    }
}
