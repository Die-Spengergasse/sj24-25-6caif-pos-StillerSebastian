using System;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe3.Commands
{
    public record UpdatePaymentItemCommand(
        [Range(1, int.MaxValue, ErrorMessage = "ID must be greater than 0.")]
        int Id,

        [Required(ErrorMessage = "ArticleName is required.")]
        [MaxLength(255, ErrorMessage = "ArticleName cannot be longer than 255 characters.")]
        string ArticleName,

        [Range(1, int.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        int Amount,

        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0.")]
        decimal Price,

        [Range(1, int.MaxValue, ErrorMessage = "PaymentId must be greater than 0.")]
        int PaymentId,

        DateTime? LastUpdated = null
    );
}

