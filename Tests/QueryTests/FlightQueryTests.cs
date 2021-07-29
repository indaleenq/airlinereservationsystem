using Application.Command;
using Application.Models;
using Application.Queries;
using Data.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace Tests.QueryTests
{
    public class FlightQueryTests
    {
        private FlightQuery _flightQuery = new FlightQuery(new JsonDataService());

        private List<FlightModel> CreateFlightModels()
        {
            var flights = new List<FlightModel>();

            flights.Add(new FlightModel()
            {
                AirlineCode = "TS",
                FlightNumber = "100",
                FlightDesignator = "TS 100",
                ArrivalStationCode = "TST",
                DepartureStationCode = "TST",
                ScheduledTimeDeparture = "15:00",
                ScheduledTimeArrival = "16:00"
            });

            flights.Add(new FlightModel()
            {
                AirlineCode = "TS",
                FlightNumber = "101",
                FlightDesignator = "TS 101",
                ArrivalStationCode = "TMN",
                DepartureStationCode = "TDV",
                ScheduledTimeDeparture = "15:00",
                ScheduledTimeArrival = "16:00"
            });

            return flights;
        }


        private void CreateTestFlightModels()
        {
            var flightCommand = new FlightCommand(new JsonDataService());
            var flightModels = CreateFlightModels();

            foreach (var flight in flightModels)
            {
                flightCommand.CreateFlight(flight);
            }
        }

        [Fact]
        public void FlightQuery_GetAllFlights()
        {
            CreateTestFlightModels();
            var flights = _flightQuery.GetFlights();

            Assert.NotNull(flights);
        }

        [Fact]
        public void FlightQuery_GetByFlightNumber()
        {
            var flights = _flightQuery.GetFlightsByFlightNumber("101");

            Assert.True(flights.Count > 0);
        }

        [Fact]
        public void FlightQuery_GetByFlightNumber_NoResults()
        {
            var flights = _flightQuery.GetFlightsByFlightNumber("500");

            Assert.True(flights.Count == 0);
        }

        [Fact]
        public void FlightQuery_GetByAirlineCode()
        {
            var flights = _flightQuery.GetFlightsByAirlineCode("TS");

            Assert.True(flights.Count > 0);
        }

        [Fact]
        public void FlightQuery_GetByAirlineCode_NoResults()
        {
            var flights = _flightQuery.GetFlightsByAirlineCode("XX");

            Assert.True(flights.Count == 0);
        }

        [Fact]
        public void FlightQuery_GetByOriginAndDestination()
        {
            var flights = _flightQuery.GetFlightsByOriginAndDestination("TST", "TST");

            Assert.True(flights.Count > 0);
        }

        [Fact]
        public void FlightQuery_GetByOriginAndDestination_NoResults()
        {
            var flights = _flightQuery.GetFlightsByOriginAndDestination("ZZZ", "ZZZ");

            Assert.True(flights.Count == 0);
        }
    }
}
