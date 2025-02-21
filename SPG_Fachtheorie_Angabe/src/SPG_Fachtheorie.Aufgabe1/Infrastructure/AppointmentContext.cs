using Bogus.DataSets;
using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{
    public class AppointmentContext : DbContext
    {
        // TODO: Add your DbSets here
        public DbSet<Model.Address> Addresses => Set<Model.Address>();
        public DbSet<CashDesk> cashDesks => Set<CashDesk>();
        public DbSet<Cashier> cashier => Set<Cashier>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Manager> Managers => Set<Manager>();
        public DbSet<Payment> Payment => Set<Payment>();
        public DbSet<PaymentItem> PaymentItems => Set<PaymentItem>();
        public AppointmentContext(DbContextOptions options)
            : base(options)
        {}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}