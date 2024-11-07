using System.ComponentModel.DataAnnotations;

namespace BusTicketingApp.Models
{
    public class BusOperator
    {
        [Key]
        public int OperatorId { get; set; }
        public string OperatorName { get; set; } = string.Empty;
        public string OperatorContact { get; set; } = string.Empty;
        
        //public Bus 


    }
}
