using Application.Models;
using System;
using System.Collections.Generic;

namespace Client
{
    public static class Common
    {
        public static string GetUserInput()
        {
            Console.Write("\nUSER INPUT: ");
            return Console.ReadLine().ToUpper();
        }

        public static void DisplayOptions(List<string> options)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("USER OPTIONS: PLEASE INPUT ANY OF THE FOLLOWING OPTIONS..");

            foreach (var option in options)
            {
                Console.WriteLine(option);
            }
        }
        public static void GetScreen(string userInput)
        {
            var screenFactory = new ScreenFactory();
            var screen = screenFactory.GetScreen(userInput);
            screen.Show();
        }

        public static string HandleInvalidInput()
        {
            Console.WriteLine("INVALID INPUT. Please try again.");
            return GetUserInput();
        }

        public static void HandlePreviousScreen(string backScreen)
        {
            Console.WriteLine("Press any key to go back");
            Console.ReadKey();
            GetScreen(backScreen);
        }

        public static void DisplayErrorMessages(List<string> messages)
        {
            foreach (var message in messages)
            {
                Console.WriteLine(message);
            }
        }

        public static void DisplayReservation(ReservationModel reservation)
        {
            Console.WriteLine($"\nRESERVATION NUMBER: {reservation.PNR}");
            Console.WriteLine($"\tFLIGHT DATE: {reservation.FlightDate.ToString("MM/dd/yyyy")}");
            Console.WriteLine("\tFLIGHT INFORMATION:");
            Console.WriteLine($"\t\tFlight Designator: {reservation.Flight.FlightDesignator}");
            Console.WriteLine($"\t\tDeparture Station: {reservation.Flight.DepartureStationCode}");
            Console.WriteLine($"\t\tArrival Station: {reservation.Flight.ArrivalStationCode}");
            Console.WriteLine($"\t\tScheduled Time Departure: {reservation.Flight.ScheduledTimeDeparture}");
            Console.WriteLine($"\t\tScheduled Time Arrival: {reservation.Flight.ScheduledTimeArrival}");

            Console.WriteLine("\tPASSENGERS INFORMATION:");
            for (int i = 1; i <= reservation.Passengers.Count; i++)
            {
                Console.WriteLine($"\t\tPassenger {i}");
                Console.WriteLine($"\t\t\tFull Name: {reservation.Passengers[i - 1].FirstName} {reservation.Passengers[i - 1].LastName}");
                Console.WriteLine($"\t\t\tBirth Date: {reservation.Passengers[i - 1].BirthDate.ToString("MM/dd/yyyy")}");
                Console.WriteLine($"\t\t\tAge: {reservation.Passengers[i - 1].Age}");
            }
        }
    }
}
