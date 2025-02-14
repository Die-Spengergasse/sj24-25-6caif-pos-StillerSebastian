using Bogus;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Employee : Person
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Employee() { }
#pragma warning restore CS8618
        public string Office { get; set; }


        public Employee(string firstName, string lastName, string accountname, string office) : base(firstName, lastName, accountname)
        {
            
            Office = office;
        }
    }
}