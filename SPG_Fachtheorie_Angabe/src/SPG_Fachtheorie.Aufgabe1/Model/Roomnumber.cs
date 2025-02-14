namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Roomnumber
    {
        public int Id { get; set; }
        public string Building { get; set; }
        public string Floor { get; set; }
        public string Number { get; set; }

    public Roomnumber(string building, string floor, string number)
        {
            Building = building;
            Floor = floor;
            Number = number;
        }
    }
}