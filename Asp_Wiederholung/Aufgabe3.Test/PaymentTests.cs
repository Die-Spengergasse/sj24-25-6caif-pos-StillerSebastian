using Xunit;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using Spg.Fachtheorie.Aufgabe3.API.Test;
using SPG_Fachtheorie.Aufgabe3.Dtos;

namespace SPG_Fachtheorie.Aufgabe3.Test
{
    public class PaymentTests : IClassFixture<TestWebApplicationFactory>
    {
        private readonly TestWebApplicationFactory _factory;

        public PaymentTests(TestWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData(1, null, 2)] // Example: 2 payments for cashDesk 1
        [InlineData(null, "2024-05-13", 3)] // Example: 3 payments from date 2024-05-13
        [InlineData(1, "2024-05-13", 1)] // Example: 1 payment for cashDesk 1 and date 2024-05-13
        public async Task GetPayments_Filtered_ReturnsExpectedCount(int? cashDesk, string? dateFrom, int expectedCount)
        {
            // Arrange
            var query = new List<string>();
            if (cashDesk.HasValue) query.Add($"cashDesk={cashDesk}");
            if (!string.IsNullOrEmpty(dateFrom)) query.Add($"dateFrom={dateFrom}");
            var requestUrl = $"/api/payments?{string.Join("&", query)}";

            // Act
            var (statusCode, payments) = await _factory.GetHttpContent<List<PaymentDto>>(requestUrl);

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.OK, statusCode);
            Assert.NotNull(payments);
            Assert.Equal(expectedCount, payments.Count);
            if (cashDesk.HasValue)
                Assert.True(payments.All(p => p.CashDesk.Number == cashDesk));
            if (!string.IsNullOrEmpty(dateFrom))
                Assert.True(payments.All(p => p.Date >= DateTime.Parse(dateFrom)));
        }
        [Theory]
        [InlineData(1, System.Net.HttpStatusCode.OK)] // Valid ID
        [InlineData(999, System.Net.HttpStatusCode.NotFound)] // Non-existent ID
        public async Task GetPaymentById_ReturnsExpectedStatusCode(int id, System.Net.HttpStatusCode expectedStatusCode)
        {
            // Arrange
            var requestUrl = $"/api/payments/{id}";

            // Act
            var (statusCode, payment) = await _factory.GetHttpContent<PaymentDto>(requestUrl);

            // Assert
            Assert.Equal(expectedStatusCode, statusCode);
            if (statusCode == System.Net.HttpStatusCode.OK)
            {
                Assert.NotNull(payment);
                Assert.Equal(id, payment.Id);
            }
        }

        [Theory]
        [InlineData(1, true, System.Net.HttpStatusCode.OK)] // Valid ID and data
        [InlineData(1, false, System.Net.HttpStatusCode.BadRequest)] // Valid ID but invalid data
        [InlineData(999, true, System.Net.HttpStatusCode.NotFound)] // Non-existent ID
        public async Task PatchPaymentById_ReturnsExpectedStatusCode(int id, bool validData, System.Net.HttpStatusCode expectedStatusCode)
        {
            // Arrange
            var requestUrl = $"/api/payments/{id}";
            var payload = validData ? new { Confirmed = true } : new { InvalidField = "Invalid" };

            // Act
            var (statusCode, _) = await _factory.PatchHttpContent(requestUrl, payload);

            // Assert
            Assert.Equal(expectedStatusCode, statusCode);
        }

        [Theory]
        [InlineData(1, System.Net.HttpStatusCode.NoContent)] // Valid ID
        [InlineData(999, System.Net.HttpStatusCode.NotFound)] // Non-existent ID
        public async Task DeletePaymentById_ReturnsExpectedStatusCode(int id, System.Net.HttpStatusCode expectedStatusCode)
        {
            // Arrange
            var requestUrl = $"/api/payments/{id}";

            // Act
            var statusCode = await _factory.DeleteHttpContent(requestUrl);

            // Assert
            Assert.Equal(expectedStatusCode, statusCode);
        }
    }
}
