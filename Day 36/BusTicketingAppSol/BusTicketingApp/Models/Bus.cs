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
        public string BusNumber { get; set; }=string.Empty;
        public BusTypes BusType {  get; set; }

        [Required(ErrorMessage ="Cannot be empty")]
        public int NumberOfSeats { get; set; }  
        public string Status { get; set; } = string.Empty;
        public decimal StandardFare { get; set; }
        public decimal PremiumFare { get; set; }
        
        public int RouteId { get; set; }
        public AvailableRoute AvailableRoutes { get; set; }

        public int OperatorID { get; set; }
        public BusOperator Operator { get; set; }

        public IEnumerable<BusSchedule> Schedules { get; set; } =new List<BusSchedule>();
        public IEnumerable<Seats> Seats { get; set; } =new List<Seats>();
       


    }
}
