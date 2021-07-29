using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IFlightQuery
    {
        List<FlightModel> GetFlights();
        List<FlightModel> GetFlightsByAirlineCode(string airlineCode);
        List<FlightModel> GetFlightsByFlightNumber(string flightNumber);
        List<FlightModel> GetFlightsByOriginAndDestination(string origin, string destination);
        List<FlightModel> GetFlightsByAirlineAndFlightNumber(string airlineCode, string flightNumber);
        FlightModel GetFlight(string flightDesignator, string departureStn, string arrivalStn);
    }
}
