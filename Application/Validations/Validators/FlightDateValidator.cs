using Application.Validations.Checks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations.Validators
{
    public class FlightDateValidator : Validator
    {
        public string FieldName { get { return "Flight Date"; } }
        private string _flightDate;
        public FlightDateValidator(string flightDate)
        {
            _flightDate = flightDate;
        }

        public override bool Execute()
        {
            Validations.Add(new CheckIfValidDateFormat(_flightDate, FieldName));
            Validations.Add(new CheckIfNotPastDate(_flightDate, FieldName));
            return IsValid;
        }
    }
}
