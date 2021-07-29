using Application.Models;
using Application.Validations.Validators;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{
    public class FlightValidator : Validator
    {
        public string FieldName { get { return "Flight"; } }
        private FlightModel _flightModel;
        private IDataService _dataService;
        public List<string> AllErrorMessages { get; private set; }

        public FlightValidator(FlightModel flightModel, IDataService dataService)
        {
            _flightModel = flightModel;
            _dataService = dataService;
            AllErrorMessages = new List<string>();
        }

        public override bool Execute()
        {
            var airlineCodeValidator = new AirlineCodeValidator(_flightModel.AirlineCode);
            var flightNumberValidator = new FlightNumberValidator(_flightModel.FlightNumber);
            var departureStnValidator = new StationCodeValidator(_flightModel.ArrivalStationCode);
            var arrivalStnValidator = new StationCodeValidator(_flightModel.DepartureStationCode);
            var staValidator = new ScheduledTimeValidator(_flightModel.ScheduledTimeArrival);
            var stdValidator = new ScheduledTimeValidator(_flightModel.ScheduledTimeDeparture);

            var isAirlineValid = airlineCodeValidator.Execute();
            var isFlightNumberValid = flightNumberValidator.Execute();
            var isDepartStnValid = departureStnValidator.Execute();
            var isArrivStnValid = arrivalStnValidator.Execute();
            var isSTAValid = staValidator.Execute();
            var isSTDStnValid = stdValidator.Execute();

            Validations.Add(new CheckIfFlightExists(_dataService, _flightModel, FieldName));

            if (isAirlineValid && isFlightNumberValid
                && isDepartStnValid && isArrivStnValid 
                && isSTAValid && isSTDStnValid && IsValid)
            {
                return true;
            }
            else
            {
                AllErrorMessages.AddRange(airlineCodeValidator.ErrorMessages);
                AllErrorMessages.AddRange(flightNumberValidator.ErrorMessages);
                AllErrorMessages.AddRange(departureStnValidator.ErrorMessages);
                AllErrorMessages.AddRange(arrivalStnValidator.ErrorMessages);
                AllErrorMessages.AddRange(staValidator.ErrorMessages);
                AllErrorMessages.AddRange(stdValidator.ErrorMessages);
                AllErrorMessages.AddRange(ErrorMessages);
                return false;
            }
        }
    }
}
