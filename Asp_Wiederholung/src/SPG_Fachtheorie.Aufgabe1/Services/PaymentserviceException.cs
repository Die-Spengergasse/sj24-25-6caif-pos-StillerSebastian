using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;


namespace SPG_Fachtheorie.Aufgabe1.Services
{
    public class PaymentserviceException : Exception
    {
        public PaymentserviceException(string message) : base(message) { }
    }
}
