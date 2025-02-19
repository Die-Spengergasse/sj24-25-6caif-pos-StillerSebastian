using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Roomnumber
    {
        public Roomnumber(string building, string floor, int number)
        {
            Building = building;
            Floor = floor;
            Number = number;
        }

        // TODO: Add your properties and constructors
        [MaxLength(2)]                        // VARCHAR(2)
        public string Building { get; set; }
        [MaxLength(2)]
        public string Floor { get; set; }
        public int Number { get; set; }
    }
}