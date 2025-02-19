using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    public class CashDesk
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long Number { get; set; }
        [MaxLength(255)]
        public 
    }
}