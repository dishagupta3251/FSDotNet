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
       
        public IEnumerable<Bus> Buses { get; set; } 


    }
}
