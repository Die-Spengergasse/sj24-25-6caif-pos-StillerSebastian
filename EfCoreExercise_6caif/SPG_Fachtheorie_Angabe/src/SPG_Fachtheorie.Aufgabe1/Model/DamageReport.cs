using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class DamageReport
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected DamageReport() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public DamageReport(Damage damage, Person reporter, DateTime dateTime)
        {
            Damage = damage;
            Reporter = reporter;
            DateTime = dateTime;
        }

        // TODO: Add your properties and constructors
        public int Id { get; set; }
        public Damage Damage { get; set; }
        public Person Reporter { get; set; }
        public DateTime DateTime { get; set; }
    }
}