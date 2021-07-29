using System;
using System.Collections.Generic;
using System.Text;
using Client;

namespace Client
{
    public class ReservationScreen : IScreen
    {
        private List<string> _options = new List<string>()
                                        {   "Type 'RC' to Create new Reservation",
                                            "Type 'RL' to List All Reservation",
                                            "Type 'RS' to Search for a Reservation by PNR",
                                            "Type 'X' to go back"};
        public void Show()
        {
            Console.Clear();
            Console.WriteLine("Airline Reservation System");
            Console.WriteLine("MAIN MENU > RESERVATIONS\n");

            Common.DisplayOptions(_options);

            var userInput = Common.GetUserInput();

            while (userInput != "X")
            {
                if (userInput == "RC" || userInput == "RL" || userInput == "RS")
                {
                    Common.GetScreen(userInput);
                }
                else
                {
                    userInput = Common.HandleInvalidInput();
                }
            }

            Program.DisplayScreen();
        }
    }
}
