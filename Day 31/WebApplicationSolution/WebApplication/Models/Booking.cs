namespace WebApplication1.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public int UserId { get; set; }
        public Event Event { get; set; }
        public User User { get; set; }

        public Booking()
        {
            Event = new Event();
            User = new User();
        }

    }
}
