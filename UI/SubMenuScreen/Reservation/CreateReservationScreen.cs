using Application.Interfaces;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Client
{
    class CreateReservationScreen : IScreen
    {
        private readonly IReservationCommand _reservationCommand;
        private readonly IFlightQuery _flightQuery;
        public CreateReservationScreen(IReservationCommand reservationCommand, IFlightQuery flightQuery)
        {
            _reservationCommand = reservationCommand;
            _flightQuery = flightQuery;
        }

        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Airline Reservation System");
            Console.WriteLine("MAIN MENU > RESERVATIONS > CREATE RESERVATION\n");

            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("CREATE RESERVATION:");

            try
            {
                var flightSelected = SearchFlight();
                var flightDate = GetFlightDateDetails();
                var flightPassengers = GetFlightPassengers();

                var reservationModel = new ReservationModel();
                reservationModel.Flight = flightSelected;
                reservationModel.FlightDate = flightDate;
                reservationModel.Passengers = flightPassengers;

                if (ConfirmFlightDetails(reservationModel))
                {
                    var createResult = _reservationCommand.CreateReservation(reservationModel);
                    if (createResult)
                    {
                        Console.WriteLine($"Successfully created reservation {_reservationCommand.PNR}.");
                        Common.HandlePreviousScreen("R");
                    }
                    else
                    {
                        Console.WriteLine("ERROR. Failed to create reservation.");
                        Common.DisplayErrorMessages(_reservationCommand.Messages);
                        Common.HandlePreviousScreen("R");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error occured: {ex.Message}");
                Common.HandlePreviousScreen("R");
            }
        }

        private FlightModel SearchFlight()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("FLIGHT DETAILS");
            Console.Write("Enter Airline Code:");
            var airlineCode = Console.ReadLine();
            Console.Write("Enter Flight Number:");
            var flightNumber = Console.ReadLine();
            var flightModel = new FlightModel();

            var flights = _flightQuery.GetFlightsByAirlineAndFlightNumber(airlineCode, flightNumber);

            if (flights.Any())
            {
                flightModel = SelectFlight(flights);
               
            }
            return flightModel;
        }

        private FlightModel SelectFlight(List<FlightModel> flights)
        {
            Console.WriteLine($"\n--------------------SEARCH RESULTS---------------------");
            Console.WriteLine($"\nFOUND {flights.Count} FLIGHT(S).");

            for (int i = 0; i < flights.Count; i++)
            {
                Console.WriteLine($"\n\tROW NUMBER: {i + 1}");
                Console.WriteLine($"\tFLIGHT {(flights[i].FlightDesignator).ToUpper()}");
                Console.WriteLine($"\t\tAirline Code:{(flights[i].AirlineCode).ToUpper()}");
                Console.WriteLine($"\t\tFlight Number:{(flights[i].FlightNumber).ToUpper()}");
                Console.WriteLine($"\t\tOrigin:{(flights[i].DepartureStationCode).ToUpper()}");
                Console.WriteLine($"\t\tDestination:{(flights[i].ArrivalStationCode).ToUpper()}");
                Console.WriteLine($"\t\tSTD:{(flights[i].ScheduledTimeDeparture)}");
                Console.WriteLine($"\t\tSTA:{(flights[i].ScheduledTimeArrival)}");
            }
            Console.WriteLine($"\n---------SELECT FLIGHT BY ENTERING ROW NUMBER---------");
            var rowNumber = 0;
            var input = Common.GetUserInput();

            while (!(Int32.TryParse(input, out rowNumber)) || rowNumber < 1 || rowNumber > flights.Count)
            {
                input = Common.HandleInvalidInput();
            }

            return _flightQuery.GetFlight(flights[rowNumber-1].FlightDesignator,
                flights[rowNumber-1].DepartureStationCode, flights[rowNumber-1].ArrivalStationCode);
        }

        private DateTime GetFlightDateDetails()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("Enter Flight Date (mm/dd/yyyy):");
            var input = Console.ReadLine();

            var isFlightDateValid = _reservationCommand.ValidateFlightDate(input);

            while (!isFlightDateValid)
            {
                var msg = _reservationCommand.Messages.Any() ? _reservationCommand.Messages.FirstOrDefault() : string.Empty;
                Console.WriteLine(msg);
                input = Common.HandleInvalidInput();
                isFlightDateValid = _reservationCommand.ValidateFlightDate(input);
            }

            return DateTime.Parse(input);
        }

        private List<PassengerModel> GetFlightPassengers()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Enter Passenger Details (only 1 to 5 passengers allowed)");
            Console.Write("How many passengers do you want to enter?: ");
            var input = Console.ReadLine();
            var passengerCount = 0;

            while (!(Int32.TryParse(input, out passengerCount)) || passengerCount < 1 || passengerCount > 5)
            {
                input = Common.HandleInvalidInput();
            }

            return GetPassengerInfo(passengerCount);
        }

        private List<PassengerModel> GetPassengerInfo(int passengerCount)
        {
            var passengers = new List<PassengerModel>();

            for (int i = 1; i <= passengerCount; i++)
            {
                Console.WriteLine("----------------------------------------------------------");
                Console.Write($"Enter Details for Passenger {i}");
                Console.Write($"\n\tPassenger {i} First Name: ");
                var firstName = Console.ReadLine();
                Console.Write($"\tPassenger {i} Last Name: ");
                var lastName = Console.ReadLine();
                Console.Write($"\tPassenger {i} Birth Date: ");
                var birthDate = GetDate();

                var passengerModel = new PassengerModel()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    BirthDate = birthDate
                };
                passengers.Add(passengerModel);
            }

            return passengers;
        }

        private DateTime GetDate()
        {
            var input = Console.ReadLine();

            var isBirthDateValid = _reservationCommand.ValidateBirthDate(input);

            while (!isBirthDateValid)
            {
                var msg = _reservationCommand.Messages.Any() ? _reservationCommand.Messages.FirstOrDefault() : string.Empty;
                Console.WriteLine(msg);
                input = Common.HandleInvalidInput();
                isBirthDateValid = _reservationCommand.ValidateBirthDate(input);
            }

            return DateTime.Parse(input);
        }

        private bool ConfirmFlightDetails(ReservationModel reservation)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("CONFIRM FLIGHT DETAILS");
            Console.WriteLine("\nFLIGHT INFORMATION");
            Console.WriteLine($"\tFlight Designator: {reservation.Flight.FlightDesignator}");
            Console.WriteLine($"\tDeparture Station: {reservation.Flight.DepartureStationCode}");
            Console.WriteLine($"\tArrival Station: {reservation.Flight.ArrivalStationCode}");
            Console.WriteLine($"\tScheduled Time Departure: {reservation.Flight.ScheduledTimeDeparture}");
            Console.WriteLine($"\tScheduled Time Arrival: {reservation.Flight.ScheduledTimeArrival}");
            
            Console.WriteLine($"FLIGHT DATE: {reservation.FlightDate.ToString("MM/dd/yyyy")}");

            Console.WriteLine("\nPASSENGERS INFORMATION");
            for (int i = 1; i <= reservation.Passengers.Count; i++)
            {
                Console.WriteLine($"\tPassenger {i}");
                Console.WriteLine($"\t\tFull Name: {reservation.Passengers[i-1].FirstName} {reservation.Passengers[i-1].LastName}");
                Console.WriteLine($"\t\tBirth Date: {reservation.Passengers[i-1].BirthDate.ToString("MM/dd/yyyy")}");
                Console.WriteLine($"\t\tAge: {reservation.Passengers[i-1].Age}");
            }

            Console.WriteLine("\n\nWould you like to proceed with the reservation(Y or N)?");
            var input = Common.GetUserInput();

            while (input != "Y" && input != "N")
            {
                input = Common.HandleInvalidInput();
            }

            return input == "Y" ? true : false;
        }
    }
}
