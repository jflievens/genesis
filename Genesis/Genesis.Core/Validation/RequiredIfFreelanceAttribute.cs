using Genesis.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace Genesis.Core.Validation
{
    public class RequiredIfFreelanceAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (validationContext.ObjectInstance is Contact)
            {
                var contact = validationContext.ObjectInstance as Contact;

                if (contact.Type == Enums.ContactType.Freelance && string.IsNullOrWhiteSpace(value.ToString()))
                {
                    return new ValidationResult("Vat number is required for Freelance contacts");
                }
            }

            return ValidationResult.Success;
        }
    }
}
