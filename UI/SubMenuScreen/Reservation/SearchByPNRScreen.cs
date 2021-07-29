using Application.Interfaces;
using Application.Models;
using System;

namespace Client
{
    class SearchByPNRScreen : IScreen
    {
        private readonly IReservationQuery _reservationQuery;
        public SearchByPNRScreen(IReservationQuery reservationQuery)
        {
            _reservationQuery = reservationQuery;
        }

        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Airline Reservation System");
            Console.WriteLine("MAIN MENU > RESERVATIONS > SEARCH RESERVATION BY PNR\n");

            try
            {
                Console.Write("Enter PNR: ");
                var pnr = Console.ReadLine();
                var reservation = _reservationQuery.GetReservation(pnr);

                if (reservation != null || reservation.PNR != null)
                {
                    DisplayReservation(reservation);
                    Common.HandlePreviousScreen("R");
                }
                else
                {
                    Console.WriteLine($"Reservation with PNR {pnr} not found.");
                    Common.HandlePreviousScreen("R");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error occured: {ex.Message}");
                Common.HandlePreviousScreen("R");
            }
            
        }

        private void DisplayReservation(ReservationModel reservation)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($"\n---------------SEARCH RESERVATIONS---------------------");
            Common.DisplayReservation(reservation);
        }

    }
}
