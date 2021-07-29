using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class PassengerModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get { return DateTime.Now.Year - BirthDate.Year; } }
        public DateTime BirthDate { get; set; }
    }
}
