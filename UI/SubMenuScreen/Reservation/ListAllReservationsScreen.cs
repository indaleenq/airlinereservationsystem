using Application.Interfaces;
using Application.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Client
{
    class ListAllReservationsScreen : IScreen
    {
        private readonly IReservationQuery _reservationQuery;
        public ListAllReservationsScreen(IReservationQuery reservationQuery)
        {
            _reservationQuery = reservationQuery;
        }

        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Airline Reservation System");
            Console.WriteLine("MAIN MENU > RESERVATIONS > LIST ALL RESERVATIONS\n");

            try
            {
                var reservations = _reservationQuery.GetReservations();

                if (reservations.Any())
                {
                    DisplayReservations(reservations);
                    Common.HandlePreviousScreen("R");
                }
                else
                {
                    Console.WriteLine("No Reservations Found.");
                    Common.HandlePreviousScreen("R");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected Error occured: {ex.Message}");
                Common.HandlePreviousScreen("R");
            }
        }

        private void DisplayReservations(List<ReservationModel> reservations)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine($"\n--------------LIST OF RESERVATIONS---------------------");
            Console.WriteLine($"\nFOUND {reservations.Count} Reservation(s).");

            foreach (var reservation in reservations)
            {
                Common.DisplayReservation(reservation);
            }

            Console.WriteLine($"\n--------------------END of RESULTS---------------------");
        }
    }
}
