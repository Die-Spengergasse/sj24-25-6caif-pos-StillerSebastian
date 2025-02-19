using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Infrastructure;
using SPG_Fachtheorie.Aufgabe1.Model;
using System;
using System.Linq;
using Xunit;

namespace SPG_Fachtheorie.Aufgabe1.Test
{
    public class Aufgabe1Test
    {
        private DamageContext GetEmptyDbContext()
        {
            var connection = new SqliteConnection("DataSource=:memory:");
            connection.Open();
            var options = new DbContextOptionsBuilder()
                .UseSqlite(connection)
                .Options;

            var db = new DamageContext(options);
            db.Database.EnsureCreated();
            return db;
        }

        [Fact]
        public void CreateDatabaseTest()
        {
            using var db = GetEmptyDbContext();
            //Assert.True(db.Employees.Count() == 0);
        }

        [Fact]
        public void AddEmployeeSuccessTest()
        {
            // TODO: Remove Exception and add your code here
            // ARRANGE
            using var db = GetEmptyDbContext();
            var employee = new Employee("FN", "LN", "ACC", 1001, "B5.18");
            // ACT
            db.Employees.Add(employee);
            db.SaveChanges();
            // ASSERT
            db.ChangeTracker.Clear();
            var employeeFromDb = db.Employees.First();
            Assert.True(employeeFromDb.Accountname == "ACC");
        }

        [Fact]
        public void AddDamageWithReportSuccessTest()
        {
            // TODO: Remove Exception and add your code here
            // ARRANGE
            using var db = GetEmptyDbContext();
            var roomnumber = new Roomnumber("A", "3", 15);
            var room = new Room(roomnumber, "WC");
            var damage = new Damage(room, "Benutztes Kondom gefunden.");

            var reporter = new Person("FN", "LN", "ACC", 10001);
            var report = new DamageReport(
                damage, reporter,
                new DateTime(2025, 2, 14, 9, 0, 0));

            // ACT
            db.Damages.Add(damage);
            db.SaveChanges();
            db.DamageReports.Add(report);
            db.SaveChanges();

            // ASSERT
            db.ChangeTracker.Clear();
            var damageReportFromDb = db.DamageReports.First();
            Assert.True(damageReportFromDb.Id != default);
        }

        [Fact]
        public void AddRepairationSuccessTest()
        {
            // TODO: Remove Exception and add your code here
            // ARRANGE
            using var db = GetEmptyDbContext();
            var roomnumber = new Roomnumber("A", "3", 15);
            var room = new Room(roomnumber, "WC");
            var damage = new Damage(room, "Benutztes Kondom gefunden.");
            var repairer = new Employee("FN", "LN", "ACC", 1001, "B5.18C");
            var repairation = new Repairation(
                damage, repairer,
                new DateTime(2025, 2, 15, 10, 0, 0),
                "Wurde an Dir. Hager übergeben.");

            // ACT
            db.Repairations.Add(repairation);
            db.SaveChanges();

            // ASSERT
            db.ChangeTracker.Clear();
            var repairationFromDb = db.Repairations.First();
            Assert.True(repairationFromDb.Description == "Wurde an Dir. Hager übergeben.");
        }

        [Fact]
        public void DiscriminatorTest()
        {
            // TODO: Remove Exception and add your code here
            // ARRANGE
            using var db = GetEmptyDbContext();
            var employee = new Employee("FN", "LN", "ACC", 1001,"B5.18C");

            // ACT
            db.Persons.Add(employee);
            db.SaveChanges();

            // ASSERT
            db.ChangeTracker.Clear();
            // SELECT * FROM Person WHERE Discriminator = 'Employee'
            var employeeFromDb = db.Employees.First();
            Assert.True(employeeFromDb.PersonType == "Employee");
        }
    }
}