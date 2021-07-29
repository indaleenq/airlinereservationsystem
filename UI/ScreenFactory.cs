using Application.Command;
using Application.Interfaces;
using Application.Queries;
using Data.Interfaces;
using System;

namespace Client
{
    public class ScreenFactory
    {
        //default value for now
        IDataService _dataService;
        IFlightQuery flightQuery;
        IFlightCommand flightCommand;
        IReservationCommand reservationCommand;
        IReservationQuery reservationQuery;
        public ScreenFactory()
        {
            _dataService = new JsonDataService();
            flightQuery = new FlightQuery(_dataService);
            flightCommand = new FlightCommand(_dataService);
            reservationCommand = new ReservationCommand(_dataService);
            reservationQuery = new ReservationQuery(_dataService);
        }

        public IScreen GetScreen(string usermenu)
        {
            switch (usermenu.ToUpper())
            {
                case "F": return new FlightManagementScreen();
                case "R": return new ReservationScreen();
                case "FC": return new CreateFlightScreen(flightCommand);
                case "FS": return new SearchFlightScreen(flightQuery);
                case "RC": return new CreateReservationScreen(reservationCommand, flightQuery);
                case "RL": return new ListAllReservationsScreen(reservationQuery);
                case "RS": return new SearchByPNRScreen(reservationQuery);
                default:
                    throw new ArgumentException($"ERROR. Invalid User Input: {usermenu}");
            }
        }
    }
}
