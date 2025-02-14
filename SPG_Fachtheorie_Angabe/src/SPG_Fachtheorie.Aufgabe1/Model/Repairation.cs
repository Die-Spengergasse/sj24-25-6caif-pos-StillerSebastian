using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{

    public class Repairation
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Repairation() { }
#pragma warning restore CS8618
        public int Id { get; set; }
        public Damage Damage { get; set; }
        public Employee Repairer { get; set; }
        public DateTime DateTime { get; set; }
        public String Description { get; set; }

        public Repairation(Damage damage, Employee repairer, DateTime dateTime, String description)
        {
            Damage = damage;
            Repairer = repairer;
            DateTime = dateTime;
            Description = description;
        }
    }
    }