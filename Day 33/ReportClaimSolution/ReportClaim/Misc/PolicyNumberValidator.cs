using System.ComponentModel.DataAnnotations;

namespace ReportClaim.Validation
{
    public class PolicyNumberValidator:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Policy Number cannot be empty");
            }

            if (value.ToString().Length < 6)
            {
                return new ValidationResult("Policy Number should be length of 6");
            }
            return ValidationResult.Success;
        }

    }
}
