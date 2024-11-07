using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace BusTicketingApp.Models
{

    public enum BusTypes
    {
        AC,
        Non_AC
    }
   
    public class Bus
    {
        [Key]
        public int BusId { get; set; }
        public string BusName { get; set; }
        public BusTypes BusType {  get; set; }
        public int NumberOfSeats { get; set; }  
        public string Status { get; set; }
        public decimal StandardFare { get; set; }
        public decimal PremiumFare { get; set; }

        public IEnumerable<BusSchedule> Schedules { get; set; } 
        public IEnumerable<Seats> Seats { get; set; } =new List<Seats>();
       


    }
}
