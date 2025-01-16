using System.ComponentModel.DataAnnotations;

namespace BankingCustomerManagement.Models.DTO
{
    public class CustomerDTO
    {
        [Required(ErrorMessage = "First name is required.")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Last name is required.")]

        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Address is required.")]

        public string Address { get; set; } = string.Empty;

        [Required(ErrorMessage = "City is required.")]

        public string City { get; set; } = string.Empty;

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }


        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
