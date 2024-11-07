using System.ComponentModel.DataAnnotations;

namespace BusTicketingApp.Models
{
 
    public class AvailableRoute
    {
        [Key]
        public int RouteId { get; set; }
        public string Origin { get; set; }=string.Empty;
        public string Destination { get; set; } = string.Empty;
        public IEnumerable<Bus> Buses { get; set; }
        
   
    }
}
