using Client;
using Data.Interfaces;
using System;
using System.Collections.Generic;

namespace Client
{
    public class Program
    {
        private static List<string> _mainOptions = new List<string>()
                                        {
                                            "Type 'F' for Flight Management",
                                            "Type 'R' for Reservations",
                                            "Type 'X' to Exit"
                                        };

        public static void Main(string[] args)
        {
            DisplayScreen();
        }

        public static void DisplayScreen()
        {
            Console.Clear();
            Console.WriteLine("\nAIRLINE RESERVATION SYSTEM");
            Console.WriteLine("MAIN MENU\n");
            Common.DisplayOptions(_mainOptions);

            var userInput = Common.GetUserInput();
            
            while (userInput != "X")
            {
                if (userInput == "F" || userInput == "R")
                {
                    Common.GetScreen(userInput);
                }
                else
                {
                    userInput = Common.HandleInvalidInput();
                }
            }
            Console.WriteLine("\nExiting application...");
            Environment.Exit(-1);
        }
    }
}