using Data.Services;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Interfaces
{
    public class JsonDataService : IDataService
    {
        //refactor
        private FlightJsonService _flightJsonService;
        private ReservationJsonService _reservationJsonService;

        public JsonDataService()
        {
            _flightJsonService = new FlightJsonService();
            _reservationJsonService = new ReservationJsonService();
        }

        public List<Flight> Flights
        {
            get
            {
                return _flightJsonService.GetFlights();
            }
        }

        public List<Reservation> Reservations
        {
            get
            {
                return _reservationJsonService.GetReservations();
            }
        }

        public void AddFlight(Flight flight)
        {
            _flightJsonService.AddFlight(flight);
        }

        public void AddReservation(Reservation reservation)
        {
            _reservationJsonService.AddReservation(reservation);
        }
    }
}
