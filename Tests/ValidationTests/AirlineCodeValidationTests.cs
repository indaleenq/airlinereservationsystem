using Application.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Tests.ValidationTests
{
    public class AirlineCodeValidationTests
    {
        private AirlineCodeValidator _airlineCodeValidator;

        [Fact]
        public void WhenAirlineCode_IsTwoLetters_Valid()
        {
            var airlineCode = "NV";
            _airlineCodeValidator = new AirlineCodeValidator(airlineCode);

            var result = _airlineCodeValidator.Execute();
            var message = _airlineCodeValidator.ErrorMessages.FirstOrDefault();

            Assert.Null(message);
            Assert.True(result);
        }

        [Fact]
        public void WhenAirlineCode_IsNumberThenLetter_Valid()
        {
            var airlineCode = "NV";
            _airlineCodeValidator = new AirlineCodeValidator(airlineCode);

            var result = _airlineCodeValidator.Execute();
            var message = _airlineCodeValidator.ErrorMessages.FirstOrDefault();

            Assert.Null(message);
            Assert.True(result);
        }

        [Fact]
        public void WhenAirlineCode_IsWhiteSpaces_NotValid()
        {
            var airlineCodeSpaces = "    ";
            _airlineCodeValidator = new AirlineCodeValidator(airlineCodeSpaces);

            var result = _airlineCodeValidator.Execute();
            var message = _airlineCodeValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Airline Code is required. It cannot be null, empty or whitespace.", message);
            Assert.False(result);
        }

        [Fact]
        public void WhenAirlineCode_IsEmptyString_NotValid()
        {
            var airlineCodeEmpty = "";
            _airlineCodeValidator = new AirlineCodeValidator(airlineCodeEmpty);

            var result = _airlineCodeValidator.Execute();

            Assert.False(result);
        }

        [Fact]
        public void WhenAirlineCode_IsLessThanTwoCharacters_NotValid()
        {
            var airlineCodeOne = "X";
            _airlineCodeValidator = new AirlineCodeValidator(airlineCodeOne);
            var result = _airlineCodeValidator.Execute();

            Assert.False(result);
        }

        [Fact]
        public void WhenAirlineCode_IsMoreThanTwoCharacters_NotValid()
        {
            var airlineCodeMoreThanTwo = "YYY";
            _airlineCodeValidator = new AirlineCodeValidator(airlineCodeMoreThanTwo);
            var result = _airlineCodeValidator.Execute();

            Assert.False(result);
        }

        [Fact]
        public void WhenAirlineCode_IsAllNumbers_NotValid()
        {
            var airlineCodeAllNumbers = "11";
            _airlineCodeValidator = new AirlineCodeValidator(airlineCodeAllNumbers);
            var result = _airlineCodeValidator.Execute();

            Assert.False(result);
        }

        [Fact]
        public void WhenAirlineCode_IsLetterThenNumber_NotValid()
        {
            var airlineCodeAllNumbers = "X1";
            _airlineCodeValidator = new AirlineCodeValidator(airlineCodeAllNumbers);
            
            var result = _airlineCodeValidator.Execute();
            var message = _airlineCodeValidator.ErrorMessages.FirstOrDefault();
           
            Assert.Equal("Airline Code should be all letters except if the first character is number."
                , message);
            Assert.False(result);
        }
    }
}
