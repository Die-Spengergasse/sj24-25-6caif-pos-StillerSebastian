using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Any;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using SPG_Fachtheorie.Aufgabe3.Dtos;

namespace SPG_Fachtheorie.Aufgabe3.Controllers
{
    [Route("api/[controller]")]  // [controller] bedeutet: das Wort vor Controller
    [ApiController]              // Soll von ASP gemappt werden
    public class PaymentController : ControllerBase
    {
        private readonly AppointmentContext _db;

        public PaymentController(AppointmentContext db)
        {
            _db = db;
        }

        /// <summary>
        /// GET /api/employees oder             --> type = null
        /// GET /api/employees?type=Manager     --> type = Manager
        /// GET /api/employees?type=manager     --> type = manager
        /// GET /api/employees?type=            --> type = ""
        /// </summary>
        [HttpGet]
        public ActionResult<List<PaymentDto>> GetAllEmployees([FromQuery] string? type)
        {
            var employees = _db.Payments
                .Include(p => p.PaymentItems)
                .Select(e => new PaymentDto(
                    e.Id,
                    e.Employee.FirstName,
                    e.Employee.LastName,
                    e.CashDesk.Number,
                    e.PaymentType.ToString(),
                    e.PaymentItems.Sum(item => item.Price * item.Amount) // Calculate total amount
                ))
                .ToList();    //  // [{...}, {...}, ... ]
            return Ok(employees);
        }



        /// <summary>
        /// GET /api/payment/{id}
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<PaymentDto> GetPayment(int id)
        {
            var payment = _db.Payments
                .Include(p => p.PaymentItems)
                .Where(p => p.Id == id)
                .Select(p => new PaymentDto(
                    p.Id,
                    p.Employee.FirstName,
                    p.Employee.LastName,
                    p.CashDesk.Number,
                    p.PaymentType.ToString(),
                    p.PaymentItems.Sum(item => item.Price * item.Amount) // Calculate total amount
                ))
                .FirstOrDefault();

            if (payment == null)
            {
                return NotFound();
            }

            return Ok(payment);
        }
        /// <summary>
        /// GET /api/payment/search?cashDeskNumber={cashDeskNumber}&dateFrom={dateFrom}
        /// </summary>
        
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<PaymentDto>> SearchPayments(int cashDeskNumber, string dateFrom)
        {
            if (!DateTime.TryParse(dateFrom, out DateTime parsedDateFrom))
            {
                return BadRequest("Invalid date format. Please use YYYY-MM-DD.");
            }

            var payments = _db.Payments
                .Include(p => p.PaymentItems)
                .Where(p => p.CashDesk.Number == cashDeskNumber && p.PaymentDateTime >= parsedDateFrom)
                .Select(p => new PaymentDto(
                    p.Id,
                    p.Employee.FirstName,
                    p.Employee.LastName,
                    p.CashDesk.Number,
                    p.PaymentType.ToString(),
                    p.PaymentItems.Sum(item => item.Price * item.Amount) // Calculate total amount
                ))
                .ToList();

            if (payments.Count == 0)
            {
                return NotFound();
            }

            return Ok(payments);
        }



    }
}
