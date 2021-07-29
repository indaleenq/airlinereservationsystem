using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    [Serializable]
    public class Reservation
    {
        public string PNR { get; set; }
        public Flight Flight { get; set; }
        public DateTime FlightDate { get; set; }
        public List<Passenger> Passengers { get; set; }
    }
}
