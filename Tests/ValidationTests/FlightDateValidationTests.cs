using Application.Validations.Validators;
using System;
using System.Linq;
using Xunit;

namespace Tests.ValidationTests
{
    public class FlightDateValidationTests
    {
        private FlightDateValidator _flightDateValidator;

        [Fact]
        public void WhenFlightDate_IsFutureDate_Valid()
        {
            var flightDate = new DateTime(2021, 08, 23);
            _flightDateValidator = new FlightDateValidator(flightDate.ToString());

            var result = _flightDateValidator.Execute();
            var message = _flightDateValidator.ErrorMessages.FirstOrDefault();

            Assert.Null(message);
            Assert.True(result);
        }

        [Fact]
        public void WhenFlightDate_IsToday_Valid()
        {
            var flightDate = DateTime.Now;
            _flightDateValidator = new FlightDateValidator(flightDate.ToString());

            var result = _flightDateValidator.Execute();
            var message = _flightDateValidator.ErrorMessages.FirstOrDefault();

            Assert.Null(message);
            Assert.True(result);
        }

        [Fact]
        public void WhenFlightDate_IsPastDate_NotValid()
        {
            var flightDate = DateTime.Now.AddDays(-1);
            _flightDateValidator = new FlightDateValidator(flightDate.ToString());

            var result = _flightDateValidator.Execute();
            var message = _flightDateValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Flight Date should be a future date.", message);
            Assert.False(result);
        }
    }
}
