using Application.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.ValidationTests
{
    public class FlightNumberValidatorTests
    {
        private FlightNumberValidator _flightNumberValidator;

        [Fact]
        public void WhenFlightNumber_IsNumeric_Valid()
        {
            var flightNumber = "27";
            _flightNumberValidator = new FlightNumberValidator(flightNumber);

            var result = _flightNumberValidator.Execute();
            var message = _flightNumberValidator.ErrorMessages.FirstOrDefault();

            Assert.Null(message);
            Assert.True(result);
        }

        [Fact]
        public void WhenFlightNumber_IsWithinRange_1_Valid()
        {
            var flightNumber = "1";
            _flightNumberValidator = new FlightNumberValidator(flightNumber);

            var result = _flightNumberValidator.Execute();
            var message = _flightNumberValidator.ErrorMessages.FirstOrDefault();

            Assert.Null(message);
            Assert.True(result);
        }

        [Fact]
        public void WhenFlightNumber_IsWithinRange_9999_Valid()
        {
            var flightNumber = "9999";
            _flightNumberValidator = new FlightNumberValidator(flightNumber);

            var result = _flightNumberValidator.Execute();
            var message = _flightNumberValidator.ErrorMessages.FirstOrDefault();

            Assert.Null(message);
            Assert.True(result);
        }

        [Fact]
        public void WhenFlightNumber_IsBelowRange_0_NotValid()
        {
            var flightNumber = "0";
            _flightNumberValidator = new FlightNumberValidator(flightNumber);

            var result = _flightNumberValidator.Execute();
            var message = _flightNumberValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Flight Number should be from 1 to 9999.", message);
            Assert.False(result);
        }

        [Fact]
        public void WhenFlightNumber_IsBeyondRange_10000_NotValid()
        {
            var flightNumber = "10000";
            _flightNumberValidator = new FlightNumberValidator(flightNumber);

            var result = _flightNumberValidator.Execute();
            var message = _flightNumberValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Flight Number should be from 1 to 9999.", message);
            Assert.False(result);
        }

        [Fact]
        public void WhenFlightNumber_IsNotANumber_NotValid()
        {
            var flightNumber = "IQ9!*";
            _flightNumberValidator = new FlightNumberValidator(flightNumber);

            var result = _flightNumberValidator.Execute();
            var message = _flightNumberValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Flight Number should be a number.", message);
            Assert.False(result);
        }
    }
}
