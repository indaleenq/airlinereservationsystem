using Application.Models;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public static class PassengerFactory
    {
        public static List<Passenger> CreateList(List<PassengerModel> passengersModel)
        {
            var passengers = new List<Passenger>();

            foreach (var passenger in passengersModel)
            {
                passengers.Add(new Passenger() {
                    FirstName = passenger.FirstName,
                    LastName = passenger.LastName,
                    BirthDate = passenger.BirthDate,
                    Age = passenger.Age
                });
            }
            return passengers;
        }
    }
}
