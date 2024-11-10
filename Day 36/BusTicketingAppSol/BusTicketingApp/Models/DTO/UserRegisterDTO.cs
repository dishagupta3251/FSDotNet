using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BusTicketingApp.Models.DTO
{
   
    public class UserRegisterDTO
    {
       
        [Required(ErrorMessage = "Name cannot be empty")]
        public string FirstName { get; set; } = string.Empty;


        [Required(ErrorMessage = "Password cannot be empty")]
        [RegularExpression(@"^[a-zA-Z0-9]*$", ErrorMessage = "Password pattern worng")]
        [DefaultValue("password")]
        [MinLength(6, ErrorMessage = "Password must be at least 6 characters long")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Cannot be empty")]
        [StringLength(10)]
        public string ContactNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Role cannot be empty")]
        public Roles Role { get; set; }
    }
}
