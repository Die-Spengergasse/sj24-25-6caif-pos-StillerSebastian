using SPG_Fachtheorie.Aufgabe1.Model;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe3.commands
{
    public record NewPaymentCommand
    (
        int cashdeskNumber,
        [WithinOneMinute]
        DateTime paymentDateTime,
        string paymentType,
        [Range(1000,9999,ErrorMessage = "Invalid RegistrationNumber")]
        Employee employeeRegistrationNumber
    );
    

    public class WithinOneMinuteAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateTime)
            {
                var now = DateTime.UtcNow;
                if (dateTime >= now.AddMinutes(-1) && dateTime <= now.AddMinutes(1))
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult("The payment date and time must be within one minute of the current time.");
                }
            }
            return new ValidationResult("Invalid date and time format.");
        }
    }

}
