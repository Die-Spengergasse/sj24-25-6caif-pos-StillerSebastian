using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;
using System.Security.Cryptography.X509Certificates;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{
    public class DamageContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Damage> Damages { get; set; }
        public DbSet<DamageReport> DamageReport { get; set; }
        public DbSet<Repairation> Repairation { get; set; }
        public DbSet<Room> rooms { get; set; }
    }


}  


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }
