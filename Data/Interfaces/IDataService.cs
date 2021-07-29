using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public interface IDataService
    {
        List<Flight> Flights { get; }
        List<Reservation> Reservations { get; }
        void AddFlight(Flight flight);
        void AddReservation(Reservation reservation);
    }
}
