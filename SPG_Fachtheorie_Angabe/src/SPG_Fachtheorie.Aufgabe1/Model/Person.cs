using Microsoft.EntityFrameworkCore;
using System;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Person
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Person() { }
#pragma warning restore CS8618

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Accountname { get; set; }

        public Person(string firstName, string lastName, string accountname)
        {
            FirstName = firstName;
            LastName = lastName;
            Accountname = accountname;
        }

    }

    
    }