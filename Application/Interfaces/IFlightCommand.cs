using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IFlightCommand
    {
        bool CreateFlight(FlightModel flightModel);
        public List<string> Messages { get; }
    }
}