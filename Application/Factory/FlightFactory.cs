using Application.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public static class FlightFactory
    {
        public static Flight Create(FlightModel flightModel)
        {
            var flight = new Flight();
            flight.AirlineCode = flightModel.AirlineCode.ToUpper();
            flight.Number = flightModel.FlightNumber.ToUpper();
            flight.FlightDesignator = (flightModel.AirlineCode + " " + flightModel.FlightNumber).ToUpper();
            flight.DepartureStationCode = flightModel.DepartureStationCode.ToUpper();
            flight.ArrivalStationCode = flightModel.ArrivalStationCode.ToUpper();
            flight.ScheduledTimeArrival = DateTime.Parse(flightModel.ScheduledTimeArrival);
            flight.ScheduledTimeDeparture = DateTime.Parse(flightModel.ScheduledTimeDeparture);
            return flight;
        }
    }
}
