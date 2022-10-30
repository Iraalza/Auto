using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auto.Messages
{
    public class NewOwnerMessage
    {
        public string Registration { get; set; }
        public string Manufacturer { get; set; }
        public string ModelCode { get; set; }
        public string ModelName { get; set; }
        public string Color { get; set; }
        public int Year { get; set; }
        public DateTime ListedAtUtc { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        public string PhoneNumber { get; set; }
    }
}
