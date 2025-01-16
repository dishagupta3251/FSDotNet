using System.ComponentModel.DataAnnotations;
using System.Drawing;

namespace BankingCustomerManagement.Models
{
    public class Customer
    {
        [Key]
        public int CustId { get; set; }

        
        public string FirstName { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string LastName { get; set; } = string.Empty;

       
        public string Address { get; set; } = string.Empty;

       

        public string City { get; set; } = string.Empty;

        public string Email { get; set; }=string.Empty;
      
        public string PhoneNumber { get; set; } = string.Empty;
        public string AccountNumber { get; set; } = string.Empty;




    }
}
