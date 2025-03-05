namespace SPG_Fachtheorie.Aufgabe3.Dtos
{
    public record PaymentDto(
           int id, string employeeFirstname,
           string employeeLastname, int cashDeskNumber ,
           string PaymentType, decimal totalAmount);
}
