using Application.Command;
using Application.Models;
using Application.Queries;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Tests.QueryTests
{
    public class ReservationQueryTests
    {
        private ReservationCommand _reservationCommand;
        private ReservationQuery _reservationQuery = new ReservationQuery(new JsonDataService());

        private ReservationModel CreateReservationModelData()
        {
            var flightModel = new FlightModel()
            {
                AirlineCode = "NV",
                FlightNumber = "100",
                FlightDesignator = "NV 100",
                ArrivalStationCode = "MNL",
                DepartureStationCode = "DVO",
                ScheduledTimeDeparture = "11:00",
                ScheduledTimeArrival = "14:20"
            };

            var passengers = new List<PassengerModel>();
            passengers.Add(new PassengerModel()
            {
                FirstName = "Harry",
                LastName = "Potter",
                BirthDate = new DateTime(1990, 07, 30)
            });
            passengers.Add(new PassengerModel()
            {
                FirstName = "Ronald",
                LastName = "Weasley",
                BirthDate = new DateTime(1991, 07, 30)
            });
            passengers.Add(new PassengerModel()
            {
                FirstName = "Hermione",
                LastName = "Granger",
                BirthDate = new DateTime(1992, 07, 30)
            });

            var reservation = new ReservationModel();
            reservation.Flight = flightModel;
            reservation.FlightDate = new DateTime(2021, 08, 30);
            reservation.Passengers = passengers;

            return reservation;
        }

        private void CreateTestReservationData()
        {
            var reservationModel = CreateReservationModelData();
            _reservationCommand = new ReservationCommand(new JsonDataService());
            _reservationCommand.CreateReservation(reservationModel);
        }

        [Fact]
        public void ReservationQuery_GetAllReservation()
        {
            CreateTestReservationData();
            var reservations = _reservationQuery.GetReservations();

            Assert.NotNull(reservations);
        }
    }
}
