using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe3.Commands
{
    public class UpdateConfirmedCommand : IValidatableObject
    {
        [Required(ErrorMessage = "Confirmed date is required.")]
        public DateTime Confirmed { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Confirmed > DateTime.UtcNow.AddMinutes(1))
            {
                yield return new ValidationResult(
                    "The confirmed date must not be more than 1 minute in the future.",
                    new[] { nameof(Confirmed) });
            }
        }
    }
}
