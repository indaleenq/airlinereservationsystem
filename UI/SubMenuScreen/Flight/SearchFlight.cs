using Application.Interfaces;
using Application.Models;
using Application.Queries;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    public class SearchFlightScreen : IScreen
    {
        private List<string> _options = new List<string>()
                                        {   "Type '1' to search flight by flight number",
                                            "Type '2' to search flight by airline code",
                                            "Type '3' to search flight by origin and destination code",
                                            "Type 'X' to go back"};

        private readonly IFlightQuery _flightQuery;
        public SearchFlightScreen(IFlightQuery flightQuery)
        {
            _flightQuery = flightQuery;
        }

        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Airline Reservation System");
            Console.WriteLine("MAIN MENU > FLIGHT MANAGEMENT > SEARCH FLIGHTS\n");

            Common.DisplayOptions(_options);

            var userInput = Common.GetUserInput();
            
            try
            {
                while (userInput != "X")
                {
                    switch (userInput)
                    {
                        case "1":
                            SearchFlightsByFlightNumber();
                            break;
                        case "2":
                            SearchFlightsByAirlineCode();
                            break;
                        case "3":
                            SearchFlightsByOriginDestination();
                            break;
                        default:
                            userInput = Common.HandleInvalidInput();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error occured: {ex.Message}");
                Common.HandlePreviousScreen("F");
            }
            Common.GetScreen("F");
        }

        private void DisplayHeader(string searchOption)
        {
            Console.Clear();
            Console.WriteLine("Airline Reservation System");
            Console.WriteLine($"FLIGHT MANAGEMENT > SEARCH FLIGHTS > BY {searchOption}\n");
            Console.WriteLine("----------------------------------------------------------");
        }

        private void DisplayFlightDetails(List<FlightModel> flights)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($"\n--------------------SEARCH RESULTS---------------------");
            Console.WriteLine($"\nFOUND {flights.Count} FLIGHT(S).");

            foreach (var flight in flights)
            {
                Console.WriteLine($"\n\tFLIGHT {(flight.FlightDesignator).ToUpper()}");
                Console.WriteLine($"\t\tAirline Code:{(flight.AirlineCode).ToUpper()}");
                Console.WriteLine($"\t\tFlight Number:{(flight.FlightNumber).ToUpper()}");
                Console.WriteLine($"\t\tOrigin:{(flight.DepartureStationCode).ToUpper()}");
                Console.WriteLine($"\t\tDestination:{(flight.ArrivalStationCode).ToUpper()}");
                //Console.WriteLine($"\t\tSTD:{(flight.ScheduledTimeDeparture.ToString(@"hh\:mm\:ss"))}");
                Console.WriteLine($"\t\tSTD:{(flight.ScheduledTimeDeparture)}");
                Console.WriteLine($"\t\tSTA:{(flight.ScheduledTimeArrival)}");
            }
            Console.WriteLine($"\n--------------------END of RESULTS---------------------");
            Common.HandlePreviousScreen("FS");
        }


        private void SearchFlightsByAirlineCode()
        {
            DisplayHeader("AIRLINE CODE");
            Console.Write("Enter Airline Code: ");
            var airlineCode = Console.ReadLine();
            var flights = _flightQuery.GetFlightsByAirlineCode(airlineCode);

            if (flights.Any())
            {
                DisplayFlightDetails(flights);
            }
            else
            {
                Console.WriteLine($"No Flights found with Airline Code {airlineCode}.");
                Common.HandlePreviousScreen("FS");
            }
        }

        private void SearchFlightsByFlightNumber()
        {
            DisplayHeader("FLIGHT NUMBER");
            Console.Write("Enter Flight Number: ");
            var flightNumber = Console.ReadLine();
            var flights = _flightQuery.GetFlightsByFlightNumber(flightNumber);

            if (flights.Any())
            {
                DisplayFlightDetails(flights);
            }
            else
            {
                Console.WriteLine($"No Flights found with Flight Number {flightNumber}.");
                Common.HandlePreviousScreen("FS");
            }
        }

        private void SearchFlightsByOriginDestination()
        {
            DisplayHeader("ORIGIN AND DESTINATION");
            Console.Write("Enter Origin: ");
            var origin = Console.ReadLine();
            Console.Write("Enter Destination: ");
            var destination = Console.ReadLine();
            var flights = _flightQuery.GetFlightsByOriginAndDestination(origin, destination);

            if (flights.Any())
            {
                DisplayFlightDetails(flights);
            }
            else
            {
                Console.WriteLine($"No Flights found with origin {origin} and destination {destination}.");
                Common.HandlePreviousScreen("FS");
            }
        }
    }
}