namespace WebApplication1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public IEnumerable<Booking> Bookings { get; set; }
        public User()
        {
            Bookings = new List<Booking>();
        }

    }
}
