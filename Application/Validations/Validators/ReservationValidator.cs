using Application.Models;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using Application.Validations.Checks;
using System.Text;

namespace Application.Validations.Validators
{
    public class ReservationValidator : Validator
    {
        public string FieldName { get { return "Reservation"; } }
        private ReservationModel _reservationModel;
        private IDataService _dataService;

        public List<string> AllErrorMessages { get; private set; }
        public ReservationValidator(ReservationModel reservationModel, IDataService dataService)
        {
            _reservationModel = reservationModel;
            _dataService = dataService;
            AllErrorMessages = new List<string>();
        }

        public override bool Execute()
        {
            var passengerValidator = new PassengerValidator(_reservationModel.Passengers);
            var flightDateValidator = new FlightDateValidator(_reservationModel.FlightDate.ToString());
            var flightExistsValidation = new CheckIfFlightExists(_dataService, _reservationModel.Flight, FieldName + " Flight");

            var isPassengerListValid = passengerValidator.Execute();
            var isFlightDateValid = flightDateValidator.Execute();
            var isFlightExists = flightExistsValidation.IsValid;

            if (isPassengerListValid && isFlightDateValid && !isFlightExists)
            {
                return true;
            }
            else
            {
                AllErrorMessages.AddRange(passengerValidator.ErrorMessages);
                AllErrorMessages.AddRange(flightDateValidator.ErrorMessages);
                if (isFlightExists)
                {
                    AllErrorMessages.Add(flightExistsValidation.Message);
                }
                AllErrorMessages.AddRange(ErrorMessages);
                return false;
            }
        }
    }
}
