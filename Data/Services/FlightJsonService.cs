using Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Data.Services
{
    public class FlightJsonService : IFlightDataService
    {
        private List<Flight> Flights { get; set; }
        private string _jsonFileName;

        public FlightJsonService()
        {
            Flights = new List<Flight>();
            _jsonFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/Data/flights.json";
            GetFlightsFromFile();
        }

        private void GetFlightsFromFile()
        {
            using (var jsonFileReader = File.OpenText(this._jsonFileName))
            {
                this.Flights = JsonSerializer.Deserialize<List<Flight>>
                    (jsonFileReader.ReadToEnd(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true})
                    .ToList();
            }


        }
        private void SaveFlightsToFile()
        {
            using (var outputStream = File.OpenWrite(_jsonFileName))
            {
                JsonSerializer.Serialize<List<Flight>>(
                    new Utf8JsonWriter(outputStream, new JsonWriterOptions
                    { SkipValidation = true, Indented = true })
                    , Flights);
            }
        }
        public void AddFlight(Flight flight)
        {
            Flights.Add(flight);
            SaveFlightsToFile();
        }
        public Flight GetFlight(string flightDesignator)
        {
            return Flights.Count > 1 ?
                    this.Flights.FirstOrDefault(x => x.FlightDesignator == flightDesignator)
                    : new Flight();
        }
        public Flight GetFlight(string flightDesignator, string departureStn, string arrivalStn)
        {
            return Flights.Count > 1
                ? this.Flights
                .FirstOrDefault(x => x.FlightDesignator == flightDesignator
                                && x.DepartureStationCode == departureStn
                                && x.ArrivalStationCode == arrivalStn)
                : new Flight();
        }
        public List<Flight> GetFlights()
        {
            return this.Flights;
        }
        public Flight GetFlightByAirlineCode(string airlineCode)
        {
            return Flights.Count > 1 ?
                     this.Flights.FirstOrDefault(x => x.AirlineCode == airlineCode)
                     : new Flight();
        }
    }
}
