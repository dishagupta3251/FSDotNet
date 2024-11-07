using System.ComponentModel.DataAnnotations;

namespace BusTicketingApp.Models
{
    public enum Roles
    {
        
        Customer,
        BusOperator,
        Admin
    }
    public class User
    {
        [Key]
        public int UserId { get; set; }

        public string FullName { get; set; }=string.Empty;

        public string Username { get; set; } = string.Empty;
        [Required(ErrorMessage = "Cannot be empty")]
        [StringLength(10)]
        public string ContactNumber {  get; set; }=string.Empty;    
        public string Email { get; set; }= string.Empty;

        public byte[] Password { get; set; }
        public byte[] PasswordHash { get; set; } 

        public Roles Role { get; set; }
        
    }
}
