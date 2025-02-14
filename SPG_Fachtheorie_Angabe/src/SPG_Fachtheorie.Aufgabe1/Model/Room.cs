using Microsoft.EntityFrameworkCore;
using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Room
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Room() { }
#pragma warning restore CS8618
        public int Id { get; set; }
        public Roomnumber Roomnumber { get; set; }
        public string Type { get; set; }

     public Room(Roomnumber roomnumber, string type)
        {
            Roomnumber = roomnumber;
            Type = type;
        }
    }
}
