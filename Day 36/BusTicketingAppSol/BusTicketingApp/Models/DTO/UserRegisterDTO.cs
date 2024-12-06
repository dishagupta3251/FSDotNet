using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BusTicketingApp.Models.DTO
{
   
    public class UserRegisterDTO
    {
       
        [Required(ErrorMessage = "Name cannot be empty")]
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password cannot be empty")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Contact cannot be empty")]
        [StringLength(10, ErrorMessage = "Contact must be at least 10 characters long")]
        public string ContactNumber { get; set; } = string.Empty;

       
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role cannot be empty")]
        public Roles Role { get; set; }
    }
}
