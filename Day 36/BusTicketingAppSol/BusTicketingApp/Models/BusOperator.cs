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
        public string Email { get; set; } = string.Empty;
        public string OperatorContact { get; set; } = string.Empty;

        public int UserId { get; set; }
        
        public User Users { get; set; }
        public IEnumerable<Review> Reviews { get; set; }
        public IEnumerable<Bus> Buses { get; set; } 


    }
}
