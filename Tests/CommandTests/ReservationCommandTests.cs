using Application.Command;
using Application.Models;
using Application.Queries;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace Tests.CommandTests
{
    public class ReservationCommandTests
    {
        private ReservationCommand _reservationCommand;

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


        [Fact]
        public void CreateReservation_ValidReservationDetails()
        {
            var reservationModel = CreateReservationModelData();
            _reservationCommand = new ReservationCommand(new JsonDataService());
            var result = _reservationCommand.CreateReservation(reservationModel);

            var reservationQuery = new ReservationQuery(new JsonDataService());
            var reservationCreated = reservationQuery.GetReservation(_reservationCommand.PNR);

            Assert.True(result);
            Assert.Equal(reservationModel.Flight.FlightDesignator, reservationCreated.Flight.FlightDesignator);
            Assert.Equal(reservationModel.Flight.ArrivalStationCode, reservationCreated.Flight.ArrivalStationCode);
            Assert.Equal(reservationModel.Flight.DepartureStationCode, reservationCreated.Flight.DepartureStationCode);
            Assert.Equal(reservationModel.FlightDate, reservationCreated.FlightDate);
            Assert.Equal(reservationModel.Passengers.Count, reservationCreated.Passengers.Count);
        }

        [Fact]
        public void CreateReservation_NotValid_FlightNotExisting()
        {
            var reservationModel = CreateReservationModelData();
            _reservationCommand = new ReservationCommand(new JsonDataService());

            //dummy not existing flight
            var flight = new FlightModel()
            {
                AirlineCode = "XX",
                FlightNumber = "0111",
                DepartureStationCode = "MNL",
                ArrivalStationCode = "MNL",
                ScheduledTimeArrival = "23:59",
                ScheduledTimeDeparture = "23:59"
            };

            reservationModel.Flight = flight;

            var result = _reservationCommand.CreateReservation(reservationModel);

            Assert.False(result);
        }

        [Fact]
        public void CreateReservation_NotValid_FlightDateIsAPastDate()
        {
            var reservationModel = CreateReservationModelData();
            _reservationCommand = new ReservationCommand(new JsonDataService());

            reservationModel.FlightDate = new DateTime(2000, 07, 27);
            var result = _reservationCommand.CreateReservation(reservationModel);

            Assert.False(result);
        }

        [Fact]
        public void CreateReservation_NotValid_PassengerWithBlankName()
        {
            var reservationModel = CreateReservationModelData();
            _reservationCommand = new ReservationCommand(new JsonDataService());

            reservationModel.Passengers.Add(new PassengerModel()
            { 
                FirstName = "",
                LastName = "",
                BirthDate = new DateTime(1999, 01,01)
            });

            reservationModel.FlightDate = new DateTime(2021, 07, 27);
            var result = _reservationCommand.CreateReservation(reservationModel);

            Assert.False(result);
        }

        [Fact]
        public void CreateReservation_NotValid_PassengerWithFutureBirthDate()
        {
            var reservationModel = CreateReservationModelData();
            _reservationCommand = new ReservationCommand(new JsonDataService());

            reservationModel.Passengers.Add(new PassengerModel()
            {
                FirstName = "",
                LastName = "Dela Cruz",
                BirthDate = new DateTime(2023, 01, 01)
            });

            reservationModel.FlightDate = new DateTime(2021, 07, 27);
            var result = _reservationCommand.CreateReservation(reservationModel);

            Assert.False(result);
        }

        [Fact]
        public void CreateReservation_NotValid_MoreThan5Pax()
        {
            var reservationModel = CreateReservationModelData();
            _reservationCommand = new ReservationCommand(new JsonDataService());

            reservationModel.Passengers.Add(new PassengerModel()
            {
                FirstName = "Juan",
                LastName = "Dela Cruz",
                BirthDate = new DateTime(1999, 01, 01)
            });

            reservationModel.Passengers.Add(new PassengerModel()
            {
                FirstName = "Jane",
                LastName = "Dela Cruz",
                BirthDate = new DateTime(2000, 10, 01)
            });

            reservationModel.Passengers.Add(new PassengerModel()
            {
                FirstName = "John",
                LastName = "Dela Cruz",
                BirthDate = new DateTime(2000, 10, 01)
            });

            reservationModel.FlightDate = new DateTime(2021, 07, 30);
            var result = _reservationCommand.CreateReservation(reservationModel);

            Assert.False(result);
        }

        [Fact]
        public void CreateReservation_NotValid_NoPax()
        {
            var reservationModel = CreateReservationModelData();
            _reservationCommand = new ReservationCommand(new JsonDataService());

            reservationModel.Passengers = new List<PassengerModel>();

            reservationModel.FlightDate = new DateTime(2021, 07, 30);
            var result = _reservationCommand.CreateReservation(reservationModel);

            Assert.False(result);
        }

    }
}
