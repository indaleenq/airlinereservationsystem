using System;
using System.Collections.Generic;
using System.Text;
using Client;

namespace Client
{
    public class FlightManagementScreen : IScreen
    {
        private List<string> _options = new List<string>()
                                        {   "Type 'FC' to Create new Flight",
                                            "Type 'FS' to Search Flights",
                                            "Type 'X' to go back" };
        public void Show()
        {
            Console.Clear();
            Console.WriteLine("\nAIRLINE RESERVATION SYSTEM");
            Console.WriteLine("MAIN MENU > FLIGHT MANAGEMENT\n");

            Common.DisplayOptions(_options);

            var userInput = Common.GetUserInput();

            while (userInput != "X")
            {
                if (userInput == "FC" || userInput == "FS")
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
