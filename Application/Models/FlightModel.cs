using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models
{
    public class FlightModel
    {
        public string AirlineCode { get; set; }
        public string FlightNumber { get; set; }
        public string FlightDesignator { get; set; }
        public string ArrivalStationCode { get; set; }
        public string DepartureStationCode { get; set; }
        public string ScheduledTimeDeparture { get; set; }
        public string ScheduledTimeArrival { get; set; }
    }
}
