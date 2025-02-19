using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    [Collection("Sequential")]
    public class Aufgabe1Test
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

        // Creates an empty DB in Debug\net8.0\cash.db
        [Fact]
        public void CreateDatabaseTest()
        {
            using var db = GetEmptyDbContext();
           
            Assert.True(db.Database.EnsureCreated());
        }

        [Fact]
        public void AddCashierSuccessTest()
        {
            using var db=GetEmptyDbContext();
            var Cashier = new Cashier();
        }

        [Fact]
        public void AddPaymentSuccessTest()
        {

        }

        [Fact]
        public void EmployeeDiscriminatorSuccessTest()
        {

        }
    }
}