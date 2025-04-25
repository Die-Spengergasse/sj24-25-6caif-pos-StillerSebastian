using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SPG_Fachtheorie.Aufgabe1.Services;
using SPG_Fachtheorie.Aufgabe3.Commands;
using SPG_Fachtheorie.Aufgabe3.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SPG_Fachtheorie.Aufgabe3.Controllers
{
    [Route("api/[controller]")]  // --> api/payments
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly Paymentservice _paymentService;

        public PaymentsController(Paymentservice paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public ActionResult<List<PaymentDto>> GetAllPayments(
            [FromQuery] int? cashDesk,
            [FromQuery] DateTime? dateFrom)
        {
            var payments = _paymentService.Payments
                .Where(p => (!cashDesk.HasValue || p.CashDesk.Number == cashDesk.Value)
                         && (!dateFrom.HasValue || p.PaymentDateTime >= dateFrom.Value))
                .Select(p => new PaymentDto(
                    p.Id, p.Employee.FirstName, p.Employee.LastName,
                    p.PaymentDateTime,
                    p.CashDesk.Number, p.PaymentType.ToString(),
                    p.PaymentItems.Sum(pi => pi.Price)))
                .ToList();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public ActionResult<PaymentDetailDto> GetPaymentById(int id)
        {
            var payment = _paymentService.Payments
                .Where(p => p.Id == id)
                .Select(p => new PaymentDetailDto(
                    p.Id, p.Employee.FirstName, p.Employee.LastName,
                    p.CashDesk.Number, p.PaymentType.ToString(),
                    p.PaymentItems
                        .Select(pi => new PaymentItemDto(
                            pi.ArticleName, pi.Amount, pi.Price))
                        .ToList()))
                .FirstOrDefault();
            if (payment is null) return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddPayment([FromBody] NewPaymentCommand cmd)
        {
            try
            {
                var payment = _paymentService.CreatePayment(cmd);
                return CreatedAtAction(nameof(AddPayment), new { payment.Id });
            }
            catch (PaymentserviceException ex)
            {
                return Problem(ex.Message, statusCode: 400);
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeletePayment(int id, [FromQuery] bool deleteItems)
        {
            try
            {
                _paymentService.DeletePayment(id, deleteItems);
                return NoContent();
            }
            catch (PaymentserviceException ex)
            {
                return Problem(ex.Message, statusCode: 400);
            }
        }

        /*  [HttpPut("/api/paymentitems/{id}")]
            [ProducesResponseType(StatusCodes.Status204NoContent)]
            [ProducesResponseType(StatusCodes.Status400BadRequest)]
            [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePaymentItem(int id, [FromBody] UpdatePaymentItemCommand cmd)
        {
            
             var paymentItem = _db.PaymentItems.FirstOrDefault(pi => pi.Id == id);
             if (paymentItem == null)
             {
                 return NotFound();
             }

             paymentItem.ArticleName = cmd.ArticleName;
             paymentItem.Amount = cmd.Amount;
             paymentItem.Price = cmd.Price;

             try
             {
                 _db.SaveChanges();
             }
             catch (DbUpdateException e)
             {
                 return Problem(e.InnerException?.Message ?? e.Message, statusCode: 400);
             }

             return NoContent();
        }*/

        [HttpPatch("/api/payments/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult ConfirmPayment(int id)
        {
            try
            {
                _paymentService.ConfirmPayment(id);
                return NoContent();
            }
            catch (PaymentserviceException ex)
            {
                return Problem(ex.Message, statusCode: 400);
            }
        }
    }
}
