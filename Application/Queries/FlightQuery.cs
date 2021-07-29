using Application.Interfaces;
using Application.Models;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Queries
{
    public class FlightQuery : IFlightQuery
    {
        private IDataService _dataService;
        public FlightQuery(IDataService dataService)
        {
            _dataService = dataService;
        }

        public List<FlightModel> GetFlights()
        {
            try
            {
                var flights = _dataService.Flights;
                return flights.Any() ? flights.Select(x => new FlightModel()
                {
                    AirlineCode = x.AirlineCode,
                    FlightNumber = x.Number,
                    FlightDesignator = x.FlightDesignator,
                    DepartureStationCode = x.DepartureStationCode,
                    ArrivalStationCode = x.ArrivalStationCode,
                    ScheduledTimeDeparture = x.ScheduledTimeDeparture.ToString(@"HH\:mm"),
                    ScheduledTimeArrival = x.ScheduledTimeArrival.ToString(@"HH\:mm")
                }).ToList() : new List<FlightModel>();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FlightModel> GetFlightsByAirlineCode(string airlineCode)
        {
            try
            {
                return GetFlights().Where(x => x.AirlineCode == airlineCode.ToUpper()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<FlightModel> GetFlightsByFlightNumber(string flightNumber)
        {
            try
            {
                return GetFlights().Where(x => x.FlightNumber == flightNumber).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<FlightModel> GetFlightsByOriginAndDestination(string origin, string destination)
        {
            try
            {
                return GetFlights().Where(x =>
                    x.DepartureStationCode == origin.ToUpper() && x.ArrivalStationCode == destination.ToUpper()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<FlightModel> GetFlightsByAirlineAndFlightNumber(string airlineCode, string flightNumber)
        {
            try
            {
                return GetFlights().Where(x =>
            x.AirlineCode == airlineCode.ToUpper() && x.FlightNumber == flightNumber.ToUpper()).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public FlightModel GetFlight(string flightDesignator, string departureStn, string arrivalStn)
        {
            try
            {
                var flights = GetFlights();
                var flight = new FlightModel();

                if (flights.Any())
                {
                    flight = flights.FirstOrDefault(x =>
                                    x.FlightDesignator == flightDesignator.ToUpper()
                                    && x.DepartureStationCode == departureStn.ToUpper()
                                    && x.ArrivalStationCode == arrivalStn.ToUpper());

                }
                return flight;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
