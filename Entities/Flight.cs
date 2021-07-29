using System;

namespace Entities
{
    [Serializable]
    public class Flight
    {
        public string Number { get; set; }
        //public Airline Airline { get; set; }
        public string AirlineCode { get; set; }
        public string FlightDesignator { get; set; }
        //public Station DepartureStation { get; set; }
        public string DepartureStationCode { get; set; }
        //public Station ArrivalStation { get; set; }
        public string ArrivalStationCode { get; set; }
        public DateTime ScheduledTimeArrival { get; set; }
        public DateTime ScheduledTimeDeparture { get; set; }
    }
}
