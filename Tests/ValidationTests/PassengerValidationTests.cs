using Application.Models;
using Application.Validations;
using Application.Validations.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Tests.ValidationTests
{
    public class PassengerValidationTests
    {
        private PassengerValidator _flightDateValidator;

        [Fact]
        public void WhenPassengers_AreLessThan5AndNamesLessThan20_Valid()
        {
            var passengers = new List<PassengerModel>();
            passengers.Add(new PassengerModel()
            {
                FirstName = "Harry",
                LastName = "Potter",
                BirthDate = new DateTime(1990, 07, 30)
            });
            passengers.Add(new PassengerModel()
            {
                FirstName = "Ronald",
                LastName = "Weasley",
                BirthDate = new DateTime(1991, 07, 30)
            });
            passengers.Add(new PassengerModel()
            {
                FirstName = "Hermione",
                LastName = "Granger",
                BirthDate = new DateTime(1992, 07, 30)
            });

            _flightDateValidator = new PassengerValidator(passengers);

            var result = _flightDateValidator.Execute();
            var message = _flightDateValidator.ErrorMessages.FirstOrDefault();

            Assert.Null(message);
            Assert.True(result);
        }

        [Fact]
        public void WhenPassengers_AreMoreThan5_NotValid()
        {
            var passengers = new List<PassengerModel>();
            passengers.Add(new PassengerModel()
            {
                FirstName = "Harry",
                LastName = "Potter",
                BirthDate = new DateTime(1990, 07, 30)
            });
            passengers.Add(new PassengerModel()
            {
                FirstName = "Ronald",
                LastName = "Weasley",
                BirthDate = new DateTime(1991, 07, 30)
            });
            passengers.Add(new PassengerModel()
            {
                FirstName = "Hermione",
                LastName = "Granger",
                BirthDate = new DateTime(1992, 07, 30)
            });
            passengers.Add(new PassengerModel()
            {
                FirstName = "Ginny",
                LastName = "Weasley",
                BirthDate = new DateTime(1993, 07, 30)
            });
            passengers.Add(new PassengerModel()
            {
                FirstName = "Luna",
                LastName = "Lovegood",
                BirthDate = new DateTime(1994, 07, 30)
            });
            passengers.Add(new PassengerModel()
            {
                FirstName = "Draco",
                LastName = "Malfoy",
                BirthDate = new DateTime(1995, 07, 30)
            });

            _flightDateValidator = new PassengerValidator(passengers);

            var result = _flightDateValidator.Execute();
            var message = _flightDateValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Number of Passenger should be 1 to 5.", message);
            Assert.False(result);
        }

        [Fact]
        public void WhenPassengers_NamesMoreThan20_NotValid()
        {
            var passengers = new List<PassengerModel>();
            passengers.Add(new PassengerModel()
            {
                FirstName = "HarryPotterRonaldWeasleyHermioneGranger",
                LastName = "Rowling",
                BirthDate = new DateTime(1990, 07, 30)
            });

            _flightDateValidator = new PassengerValidator(passengers);

            var result = _flightDateValidator.Execute();
            var message = _flightDateValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Passenger First Name should be not more than 20 characters.", message);
            Assert.False(result);
        }
        [Fact]
        public void WhenPassengers_IsEmpty_NotValid()
        {
            var passengers = new List<PassengerModel>();
            _flightDateValidator = new PassengerValidator(passengers);

            var result = _flightDateValidator.Execute();
            var message = _flightDateValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Number of Passenger should be 1 to 5.", message);
            Assert.False(result);
        }
    }
}
