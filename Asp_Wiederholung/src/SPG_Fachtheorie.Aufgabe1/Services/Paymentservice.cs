using SPG_Fachtheorie.Aufgabe3.commands;
using System;
using System.Collections.Generic;
using System.Linq;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;

// Rest of the file content remains unchanged
namespace SPG_Fachtheorie.Aufgabe1.Services
    {
    public class Paymentservice
    {
        private readonly AppointmentContext _db;

        public Paymentservice(AppointmentContext db)
        {
            _db = db;
        }

        public Payment CreatePayment(NewPaymentCommand cmd)
        {
            var cashDesk = _db.CashDesks.FirstOrDefault(c => c.Number == cmd.CashDeskNumber);
            if (cashDesk == null) throw new PaymentserviceException("Invalid cashdesk");

            var employee = _db.Employees.FirstOrDefault(e => e.RegistrationNumber == cmd.EmployeeRegistrationNumber);
            if (employee == null) throw new PaymentserviceException("Invalid employee");

            var existingPayment = _db.Payments.FirstOrDefault(p => p.CashDesk.Number == cmd.CashDeskNumber && p.Confirmed == null);
            if (existingPayment != null) throw new PaymentserviceException("Open payment for cashdesk.");

            if (cmd.PaymentType == "CreditCard" && employee.GetType() != typeof(Manager))
            {
                throw new PaymentserviceException("Insufficient rights to create a credit card payment.");
            }

            var paymentType = Enum.Parse<PaymentType>(cmd.PaymentType);
            var payment = new Payment(cashDesk, DateTime.UtcNow, employee, paymentType);
            _db.Payments.Add(payment);
            _db.SaveChanges();

            return payment;
        }

        public void ConfirmPayment(int paymentId)
        {
            var payment = _db.Payments.FirstOrDefault(p => p.Id == paymentId);
            if (payment == null) throw new PaymentserviceException("Payment not found");

            if (payment.Confirmed != null) throw new PaymentserviceException("Payment already confirmed");

            payment.Confirmed = DateTime.UtcNow;
            _db.SaveChanges();
        }

        public void AddPaymentItem(NewPaymentItemCommand cmd)
        {
            var payment = _db.Payments.FirstOrDefault(p => p.Id == cmd.PaymentId);
            if (payment == null) throw new PaymentserviceException("Payment not found");

            if (payment.Confirmed != null) throw new PaymentserviceException("Payment already confirmed");

            var paymentItem = new PaymentItem(cmd.ArticleName, cmd.Amount, cmd.Price, payment);
            _db.PaymentItems.Add(paymentItem);
            _db.SaveChanges();
        }

        public void DeletePayment(int paymentId, bool deleteItems)
        {
            var payment = _db.Payments.FirstOrDefault(p => p.Id == paymentId);
            if (payment == null) throw new PaymentserviceException("Payment not found");

            var paymentItems = _db.PaymentItems.Where(p => p.Payment.Id == paymentId).ToList();

            if (deleteItems)
            {
                _db.PaymentItems.RemoveRange(paymentItems);
            }
            else if (paymentItems.Any())
            {
                throw new PaymentserviceException("Payment has payment items.");
            }

            _db.Payments.Remove(payment);
            _db.SaveChanges();
        }
    }
    }

