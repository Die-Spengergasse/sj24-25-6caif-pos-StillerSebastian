namespace SPG_Fachtheorie.Aufgabe1.Model
{
    /// <summary>
    /// Das enum ist ein symbolischer Wert für eine Zahl.
    /// Ich kann z. B. 
    ///     if (damage.Status == RepairStatus.Repaired)
    /// schreiben.
    /// </summary>
    public enum RepairStatus 
    {
        // TODO: Add your properties
        Reported,            // 0
        PendingRepair,       // 1
        Repaired             // 2
    }
}