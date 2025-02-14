namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class Damage
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Damage() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        public int Id { get; set; }
        public Room Room { get; set; }
      public string Description { get; set; }
      public RepairStatus Status { get; set; }

      public Damage(Room room, string description, RepairStatus status)
        {
            Room = room;
            Description = description;
            Status = status;
        }
    }
}