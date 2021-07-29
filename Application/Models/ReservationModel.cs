using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class ReservationModel
    {
        public string PNR { get; set; }
        public FlightModel Flight { get; set; }
        public DateTime FlightDate { get; set; }
        public List<PassengerModel> Passengers { get; set; }
    }
}
