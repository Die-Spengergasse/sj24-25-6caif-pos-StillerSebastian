using System.ComponentModel.DataAnnotations;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Damage
    {
        #pragma warning disable CS8618
        protected Damage() { }
        #pragma warning restore CS8618
        public Damage(Room room, string description)
        {
            Room = room;
            Description = description;
            // Wird ein neuer Schaden angelegt, hat dieser den Status
            // Reported, der im Konstruktor gesetzt wird.
            Status = RepairStatus.Reported;
        }

        // TODO: Add your properties and constructors
        // By convention ist ein Property mit dem Namen
        // Id der Primary key.
        // By convention ist der Typ int bei einem PK
        // ein AUTO_INCREMENT Wert.
        public int Id { get; set; }
        public Room Room { get; set; }
        [MaxLength(4096)]
        public string Description { get; set; }
        public RepairStatus Status { get; set; }
    }
}