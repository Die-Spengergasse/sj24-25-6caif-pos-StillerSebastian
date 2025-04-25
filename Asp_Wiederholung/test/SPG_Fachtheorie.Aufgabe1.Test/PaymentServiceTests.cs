using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Commands;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using SPG_Fachtheorie.Aufgabe1.Services;
using System;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    public class PaymentServiceTests
    {
        private AppointmentContext GetEmptyDbContext()
        {
            var options = new DbContextOptionsBuilder()
                .UseSqlite(@"Data Source=cash.db")
                .Options;

            var db = new AppointmentContext(options);
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void CreatePaymentThrowsExceptionForNullCommand()
        {
            // Arrange
            var dbContext = GetEmptyDbContext();
            var paymentService = new PaymentService(dbContext);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                paymentService.CreatePayment(null!); // Fixed: Added null-forgiving operator
            });

            Assert.Equal("Command cannot be null.", exception.Message);
        }

        [Fact]
        public void CreatePaymentSuccessTest()
        {
            // Arrange
            var dbContext = GetEmptyDbContext();
            var paymentService = new PaymentService(dbContext);
            
            var validCommand = new NewPaymentCommand
            {
                CashDesk = new CashDesk(1), // Fixed: Corrected syntax to initialize properties
                PaymentDateTime = DateTime.Now,
                Employee = new Cashier(1, "John", "Doe", new DateOnly(1980, 1, 1), 3000, null, "General"),
                PaymentType = PaymentType.Cash
            };

            // Act
            var result = paymentService.CreatePayment(validCommand);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Payment>(result);
            Assert.Equal(validCommand.CashDesk, result.CashDesk);
            Assert.Equal(validCommand.PaymentDateTime, result.PaymentDateTime);
            Assert.Equal(validCommand.Employee, result.Employee);
            Assert.Equal(validCommand.PaymentType, result.PaymentType);
        }

        [Fact]
        public void DeletePaymentThrowsExceptionForInvalidId()
        {
            // Arrange
            var dbContext = GetEmptyDbContext();
            var paymentService = new PaymentService(dbContext);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                paymentService.DeletePayment(-1, deleteItems: true);
            });

            Assert.Equal("Payment not found.", exception.Message);
        }

        [Fact]
        public void ConfirmPaymentThrowsExceptionForInvalidId()
        {
            // Arrange
            var dbContext = GetEmptyDbContext();
            var paymentService = new PaymentService(dbContext);

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() =>
            {
                paymentService.ConfirmPayment(-1);
            });

            Assert.Equal("Payment not found.", exception.Message);
        }
    }
}
