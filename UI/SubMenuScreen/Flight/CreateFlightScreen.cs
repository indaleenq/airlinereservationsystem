using Application.Interfaces;
using Application.Models;
using System;
using System.Collections.Generic;

namespace Client
{
    public class CreateFlightScreen : IScreen
    {
        private readonly IFlightCommand _flightCommand;
        public CreateFlightScreen(IFlightCommand flightCommand)
        {
            _flightCommand = flightCommand;
        }

        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Airline Reservation System");
            Console.WriteLine("MAIN MENU > FLIGHT MANAGEMENT > CREATE FLIGHT\n");

            try
            {
                var flightDetails = GetFlightDetails();
                var createResult = _flightCommand.CreateFlight(flightDetails);

                if (createResult)
                {
                    Console.WriteLine($"Flight {flightDetails.FlightDesignator} successfully created.");
                    Common.HandlePreviousScreen("F");
                }
                else
                {
                    Console.WriteLine("ERROR. Failed to create flight.");
                    Common.DisplayErrorMessages(_flightCommand.Messages);
                    Common.HandlePreviousScreen("F");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error occured: {ex.Message}");
                Common.HandlePreviousScreen("F");
            }
        }

        private FlightModel GetFlightDetails()
        {
            Console.WriteLine("----------------------------------------------------------");
            var flightModel = new FlightModel();
            Console.WriteLine("CREATE NEW FLIGHT:");
            Console.WriteLine("Enter the flight details: ");
            
            try
            {
                Console.Write("\tAirline Code: ");
                flightModel.AirlineCode = Console.ReadLine();

                Console.Write("\tFlight Number: ");
                flightModel.FlightNumber = Console.ReadLine();

                Console.Write("\tORIGIN STATION CODE: ");
                flightModel.DepartureStationCode = Console.ReadLine();

                Console.Write("\tDESTINATION STATION CODE: ");
                flightModel.ArrivalStationCode = Console.ReadLine();

                //catch runtime error -- this should be done in a validator
                Console.Write("\tTIME OF DEPARTURE (hh:mm): ");
                flightModel.ScheduledTimeDeparture = Console.ReadLine();

                //catch runtime error -- this should be done in a validator
                Console.Write("\tTIME OF ARRIVAL (hh:mm): ");
                flightModel.ScheduledTimeArrival = Console.ReadLine();
                Console.WriteLine("----------------------------------------------------------");

                return flightModel;
            }
            catch (FormatException timeFormatEx)
            {
                Console.WriteLine($"INVALID TIME INPUT. {timeFormatEx.Message}");
                Console.WriteLine("Press any key to clear details..");
                Console.ReadKey();
                Show();
                return flightModel;
            }
        }
    }
}
