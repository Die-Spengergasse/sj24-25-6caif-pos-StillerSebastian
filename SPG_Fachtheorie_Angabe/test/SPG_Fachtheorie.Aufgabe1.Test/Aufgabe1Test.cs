using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    [Collection("Sequential")]
    public class Aufgabe1Test
    {
        private DamageContext GetEmptyDbContext()
        {
            // Database created in C:\Scratch\Aufgabe1_Test\Debug\net8.0\damages.db
            var options = new DbContextOptionsBuilder()
                .UseSqlite(@"Data Source=damages.db")
                .Options;

            var db = new DamageContext();
            db.Database.EnsureDeleted();
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void CreateDatabaseTest()
        {
            using var db = GetEmptyDbContext();
            Assert.True(db.Employees.Count() == 0);
        }

        [Fact]
        public void AddEmployeeSuccessTest()
        {
            // TODO: Remove Exception and add your code here
            using var db = GetEmptyDbContext();

        }

        [Fact]
        public void AddDamageWithReportSuccessTest()
        {
            // TODO: Remove Exception and add your code here
            using var db = GetEmptyDbContext();
            var roomnumber = db.roomnumbers.Add(new Roomnumber("A","2","15"));
            var staff = db.Employees.Add(new Employee("John", "Doe","JohnDoe","A2.15"));
            var room = db.rooms.Add(new Room(roomnumber, "Stammklasse"));
            var damage = db.Damages.Add(new Damage(room, "A broken", "Reported"));
            var report = db.DamageReport.Add(new DamageReport(damage, staff, DateTime.Now));    
        }

        [Fact]
        public void AddRepairationSuccessTest()
        {
            // TODO: Remove Exception and add your code here
            throw new NotImplementedException();
        }
    }
}