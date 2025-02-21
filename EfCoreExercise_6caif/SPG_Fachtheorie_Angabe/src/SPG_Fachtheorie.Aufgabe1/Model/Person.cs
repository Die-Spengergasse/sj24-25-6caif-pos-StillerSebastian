using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SPG_Fachtheorie.Aufgabe1.Model
{
    [Index(nameof(Accountname), IsUnique = true)]
    public class Person
    {
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        protected Person() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.
        // var p = new Person("Fn", "LN", "ACC1234");
        public Person(
            string firstname, string lastname,
            string accountname, long socialInsuranceNumber)
        {
            Firstname = firstname;
            Lastname = lastname;
            Accountname = accountname;
            SocialInsuranceNumber = socialInsuranceNumber;
        }

        // Wenn ich einen Key haben möchte, der nicht Id heißt, brauche ich die [Key] Annotation
        // Wenn ich kein auto_increment haben möchte, brauche ich DatabaseGeneratedOption.None
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long SocialInsuranceNumber { get; set; }
        // TODO: Add your properties and constructors
        [MaxLength(255)]
        public string Firstname { get; set; }
        [MaxLength(255)]
        public string Lastname { get; set; }
        [MaxLength(16)]
        public string Accountname { get; set; }
        // Wir wollen den discriminator in das Feld meppen
        // Da EF Core das Feld schreibt, weisen wir mit null! einen null Wert zu.
        // Vorsicht: string.Empty funktioniert nicht!
        public string PersonType { get; set; } = null!;
    }
}