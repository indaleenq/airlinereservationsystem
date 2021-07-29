using Application.Validations;
using System.Linq;
using Xunit;

namespace Tests.ValidationTests
{
    public class StationCodeValidationTests
    {
        private StationCodeValidator _stationCodeValidator;

        [Fact]
        public void WhenStationCode_IsThreeCharacters_Valid()
        {
            var airlineCode = "MNL";
            _stationCodeValidator = new StationCodeValidator(airlineCode);

            var result = _stationCodeValidator.Execute();
            var message = _stationCodeValidator.ErrorMessages.FirstOrDefault();

            Assert.Null(message);
            Assert.True(result);
        }

        [Fact]
        public void WhenStationCode_IsLessThanThreeCharacters_NotValid()
        {
            var airlineCode = "MN";
            _stationCodeValidator = new StationCodeValidator(airlineCode);

            var result = _stationCodeValidator.Execute();
            var message = _stationCodeValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Station Code should be 3 characters.", message);
            Assert.False(result);
        }

        [Fact]
        public void WhenStationCode_IsMoreThanThreeCharacters_NotValid()
        {
            var airlineCode = "MNLL";
            _stationCodeValidator = new StationCodeValidator(airlineCode);

            var result = _stationCodeValidator.Execute();
            var message = _stationCodeValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Station Code should be 3 characters.", message);
            Assert.False(result);
        }

        [Fact]
        public void WhenStationCode_IsEmpty_NotValid()
        {
            var airlineCode = "";
            _stationCodeValidator = new StationCodeValidator(airlineCode);

            var result = _stationCodeValidator.Execute();
            var message = _stationCodeValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Station Code is required. It cannot be null, empty or whitespace.", message);
            Assert.False(result);
        }

        [Fact]
        public void WhenStationCode_IsSpaces_NotValid()
        {
            var airlineCode = "    ";
            _stationCodeValidator = new StationCodeValidator(airlineCode);

            var result = _stationCodeValidator.Execute();
            var message = _stationCodeValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Station Code is required. It cannot be null, empty or whitespace.", message);
            Assert.False(result);
        }

        [Fact]
        public void WhenStationCode_DoesNotStartWithALetter_NotValid()
        {
            var airlineCode = "11I";
            _stationCodeValidator = new StationCodeValidator(airlineCode);

            var result = _stationCodeValidator.Execute();
            var message = _stationCodeValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Station Code should be an alphanumeric that starts with a letter.", message);
            Assert.False(result);
        }

        [Fact]
        public void WhenStationCode_ContainsSpecialCharacters_NotValid()
        {
            var airlineCode = "I!*";
            _stationCodeValidator = new StationCodeValidator(airlineCode);

            var result = _stationCodeValidator.Execute();
            var message = _stationCodeValidator.ErrorMessages.FirstOrDefault();

            Assert.Equal("Station Code should be an alphanumeric that starts with a letter.", message);
            Assert.False(result);
        }
    }
}
