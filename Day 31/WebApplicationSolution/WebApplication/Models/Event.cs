using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Event
    {
        [Key]
        public int EventId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime Time { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public IEnumerable<Booking> Bookings { get; set; }

        public Event()
        {
            Bookings = new List<Booking>();
        }

    }
}
