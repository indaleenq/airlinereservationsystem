using Application.Validations.Checks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{
    public class FlightNumberValidator : Validator
    {
        public string FieldName { get { return "Flight Number"; } }
        private string _flightNumber;

        public FlightNumberValidator(string flightNumber)
        {
            _flightNumber = flightNumber;
        }

        public override bool Execute()
        {
            Validations.Add(new CheckIfNullEmptySpace(_flightNumber, FieldName));
            Validations.Add(new CheckIfNumbersOnly(_flightNumber, FieldName));
            Validations.Add(new CheckIfNumberIsWithinRange(_flightNumber, FieldName, 1, 9999));
            Validations.Add(new CheckMaxLength(_flightNumber, FieldName, 4));

            return IsValid;
        }
    }
}
