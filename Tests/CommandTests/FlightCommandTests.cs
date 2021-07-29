using Application.Command;
using Application.Models;
using Application.Queries;
using Data.Interfaces;
using System;
using System.Linq;
using Xunit;

namespace Tests.CommandTests
{
    public class FlightCommandTests
    {
        private FlightCommand _flightCommand;

        private FlightModel CreateFlightModel
            (string airlinecode, string flightnumber, string departStn, string arrivalStn)
        {
           return new FlightModel()
            {
                AirlineCode = airlinecode,
                FlightNumber = flightnumber,
                FlightDesignator = $"{airlinecode} {flightnumber}",
                ArrivalStationCode = arrivalStn,
                DepartureStationCode = departStn,
                ScheduledTimeDeparture = "10:00",
                ScheduledTimeArrival = "10:30"
            };
        }

        private FlightModel CreateTestData1()
        {
            return new FlightModel()
            {
                AirlineCode = "NV",
                FlightNumber = "100",
                FlightDesignator = "NV 100",
                ArrivalStationCode = "MNL",
                DepartureStationCode = "DVO",
                ScheduledTimeDeparture = "11:00",
                ScheduledTimeArrival = "14:20"
            };
        }
        private FlightModel CreateTestData2()
        {
            return new FlightModel()
            {
                AirlineCode = "NV",
                FlightNumber = "1020",
                FlightDesignator = "NV 1020",
                ArrivalStationCode = "DVO",
                DepartureStationCode = "MNL",
                ScheduledTimeDeparture = "21:00",
                ScheduledTimeArrival = "01:20"
            };
        }

        [Fact]
        public void CreatingFlight_Valid_FlightDetails()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateTestData1();
            _flightCommand.CreateFlight(flightModel);

            var flightQuery = new FlightQuery(new JsonDataService());
            var flightAdded = flightQuery
                .GetFlight(flightModel.FlightDesignator, flightModel.DepartureStationCode, flightModel.ArrivalStationCode);

            Assert.Equal(flightModel.AirlineCode, flightAdded.AirlineCode);
            Assert.Equal(flightModel.FlightNumber, flightAdded.FlightNumber);
            Assert.Equal(flightModel.FlightDesignator, flightAdded.FlightDesignator);
            Assert.Equal(flightModel.DepartureStationCode, flightAdded.DepartureStationCode);
            Assert.Equal(flightModel.ArrivalStationCode, flightAdded.ArrivalStationCode);
            Assert.Equal(flightModel.ScheduledTimeDeparture, flightAdded.ScheduledTimeDeparture);
            Assert.Equal(flightModel.ScheduledTimeArrival, flightAdded.ScheduledTimeArrival);
        }

        [Fact]
        public void CreatingFlight_NotValid_AlreadyExists()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel1 = CreateTestData2();
            _flightCommand.CreateFlight(flightModel1);

            var flightModel2 = CreateTestData2();
            var result = _flightCommand.CreateFlight(flightModel2);
            var message = _flightCommand.Messages.FirstOrDefault();

            Assert.Equal("Flight already exists.", message);
            Assert.False(result);
        }

        //Airline Code Tests
        [Fact]
        public void CreatingFlight_Valid_AirlineCode_NumberLetter()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("5J", "0001", "CRK", "DVO");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.True(result);
        }

        [Fact]
        public void CreatingFlight_Vaid_WithAirlineCode_LetterLetter()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "0727", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.True(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_AirlineCode_LetterNumber()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("J5", "0001", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_AirlineCode_Blank()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("", "0001", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_AirlineCode_LessThan2Chars()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("X", "0001", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_AirlineCode_MoreThan2Chars()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("XYZ", "0001", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_AirlineCode_NumberNumber()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("27", "0001", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_AirlineCode_WithInvalidCharacters()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("%*", "0001", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        //Flight Number Tests

        [Fact]
        public void CreatingFlight_Valid_FlightNumber_ValueWithin1to9999()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "9999", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.True(result);
        }

        [Fact]
        public void CreatingFlight_Valid_FlightNumber_4Chars()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "1915", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.True(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_FlightNumber_ValueBeyond9999()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "10000", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_FlightNumber_ValueLessThan1()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "0000", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_FlightNumber_Blank()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_FlightNumber_MoreThan4Chars()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "12345", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_FlightNumber_WithInvalidCharacters()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "*./2", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        //Station Code Tests

        [Fact]
        public void CreatingFlight_Valid_StationCodes_3Chars()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("NV", "1992", "BKK", "MNL");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.True(result);
        }

        [Fact]
        public void CreatingFlight_Valid_StationCodes_StartsWithLetter()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "0715", "B5L", "M77");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.True(result);
        }

        [Fact]
        public void CreatingFlight_Valid_StationCodes_LetterNumberNumber()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "0715", "X55", "Z77");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.True(result);
        }

        [Fact]
        public void CreatingFlight_Valid_StationCodes_AllLetters()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "0715", "MNL", "BKK");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.True(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_StationCodes_LessThan3()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "0715", "BK", "MN");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_StationCodes_MoreThan3()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "0715", "BKKS", "MNLA");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_StationCodes_Blank()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "0715", "", "");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_StationCodes_NotStartWithLetter()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "0715", "5ZZ", "7YY");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValid_StationCodes_AllNumbers()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = CreateFlightModel("TG", "0715", "555", "777");
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        //Time
        [Fact]
        public void CreatingFlight_ValidFormat_STDSTA()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel =  new FlightModel()
            {
                AirlineCode = "NV",
                FlightNumber = "9998",
                FlightDesignator = "NV 9998",
                ArrivalStationCode = "MNL",
                DepartureStationCode = "DVO",
                ScheduledTimeDeparture = "20:25",
                ScheduledTimeArrival = "03:30"
            };
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.True(result);
        }

        [Fact]
        public void CreatingFlight_NotValidFormat_STD()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = new FlightModel()
            {
                AirlineCode = "NV",
                FlightNumber = "9998",
                FlightDesignator = "NV 9998",
                ArrivalStationCode = "MNL",
                DepartureStationCode = "DVO",
                ScheduledTimeDeparture = "8:20 PM",
                ScheduledTimeArrival = "03:30"
            };
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }

        [Fact]
        public void CreatingFlight_NotValidFormat_STA()
        {
            _flightCommand = new FlightCommand(new JsonDataService());
            var flightModel = new FlightModel()
            {
                AirlineCode = "NV",
                FlightNumber = "9998",
                FlightDesignator = "NV 9998",
                ArrivalStationCode = "MNL",
                DepartureStationCode = "DVO",
                ScheduledTimeDeparture = "20:25",
                ScheduledTimeArrival = "330"
            };
            var result = _flightCommand.CreateFlight(flightModel);

            Assert.False(result);
        }
    }
}
