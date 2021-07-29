using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IFlightDataService
    {
        List<Flight> GetFlights();

        Flight GetFlight(string flightDesignator);

        void AddFlight(Flight flight);

        Flight GetFlightByAirlineCode(string airlineCode);
    }
}
