using Microsoft.EntityFrameworkCore;
using SPG_Fachtheorie.Aufgabe1.Model;

namespace SPG_Fachtheorie.Aufgabe1.Infrastructure
{
    public class DamageContext : DbContext
    {
        // TODO: Add your DbSets
        // NUR für Entity Klassen im Modell wird ein DbSet registriert.
        public DbSet<Damage> Damages => Set<Damage>();
        public DbSet<DamageReport> DamageReports => Set<DamageReport>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Repairation> Repairations => Set<Repairation>();
        public DbSet<Room> Rooms => Set<Room>();

        public DamageContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // TODO: Add additional configuration
            // Definiert Roomnumber als value object
            modelBuilder.Entity<Room>().OwnsOne(r => r.Roomnumber);
            // Speichert die Enum in Damage als String
            modelBuilder.Entity<Damage>()
                .Property(d => d.Status).HasConversion<string>();
            // EF Core soll das Feld Discriminator in das Property PersonType schreiben
            modelBuilder.Entity<Person>().HasDiscriminator(p => p.PersonType);
        }
    }
}