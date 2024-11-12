using System.ComponentModel.DataAnnotations;

namespace BusTicketingApp.Models
{
    public class BusOperator
    {
        [Key]
        public int OperatorId { get; set; }
        
        public string OperatorName { get; set; }
        public string LicenseNumber { get; set; } = string.Empty;
        public string CompanyName { get; set; } = string.Empty;
        public string OperatorContact { get; set; } = string.Empty;
        
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Bus> Buses { get; set; } 


    }
}
