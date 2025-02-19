using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Employee : Person
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Employee() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public Employee(
            string firstname, string lastname, string accountname, long socialInsuranceNumber,
            string office) : base(firstname, lastname, accountname, socialInsuranceNumber)
        {
            Office = office;
        }

        // Kein Property ID, denn es wird von Person vererbt!
        // TODO: Add your properties and constructors
        [MaxLength(16)]
        public string Office { get; set; }
    }
}