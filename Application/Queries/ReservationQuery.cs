using Application.Interfaces;
using Application.Models;
using Data.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Application.Queries
{
    public class ReservationQuery : IReservationQuery
    {
        private IDataService _dataService;

        public ReservationQuery(IDataService dataService)
        {
            _dataService = dataService;
        }

        public ReservationModel GetReservation(string pnr)
        {
            try
            {
                var reservations = GetReservations();
                var reservation = new ReservationModel();

                if (reservations.Any())
                {
                    reservation = reservations.FirstOrDefault(x => x.PNR == pnr);
                }

                return reservation;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }

        public List<ReservationModel> GetReservations()
        {
            try
            {
                var reservations = _dataService.Reservations;
                var reservationModels = new List<ReservationModel>();

                if (reservations.Any())
                {
                    foreach (var reservation in reservations)
                    {
                        var flight = new FlightModel()
                        {
                            AirlineCode = reservation.Flight.AirlineCode,
                            FlightNumber = reservation.Flight.Number,
                            FlightDesignator = reservation.Flight.FlightDesignator,
                            ArrivalStationCode = reservation.Flight.ArrivalStationCode,
                            DepartureStationCode = reservation.Flight.DepartureStationCode,
                            ScheduledTimeArrival = reservation.Flight.ScheduledTimeArrival.ToString(@"HH\:mm"),
                            ScheduledTimeDeparture = reservation.Flight.ScheduledTimeDeparture.ToString(@"HH\:mm")
                        };

                        var passengers = new List<PassengerModel>();
                        foreach (var passenger in reservation.Passengers)
                        {
                            passengers.Add(new PassengerModel()
                            {
                                FirstName = passenger.FirstName,
                                LastName = passenger.LastName,
                                BirthDate = passenger.BirthDate,
                            });
                        }

                        reservationModels.Add(new ReservationModel()
                        {
                            PNR = reservation.PNR,
                            Flight = flight,
                            FlightDate = reservation.FlightDate,
                            Passengers = passengers
                        });
                    }
                }

                return reservationModels;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
