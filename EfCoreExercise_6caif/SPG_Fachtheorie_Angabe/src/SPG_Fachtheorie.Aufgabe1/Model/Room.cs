using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Room
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Room() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Room(Roomnumber roomnumber, string type)
        {
            Roomnumber = roomnumber;
            Type = type;
        }

        // TODO: Add your properties and constructors
        public int Id { get; set; }
        public Roomnumber Roomnumber { get; set; }
        [MaxLength(255)]
        public string Type { get; set; }
    }
}
