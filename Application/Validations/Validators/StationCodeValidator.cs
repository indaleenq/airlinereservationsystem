using Application.Validations.Checks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{
    public class StationCodeValidator : Validator
    {
        public string FieldName { get { return "Station Code"; } }
        private string _stationCode;
        public StationCodeValidator(string stationCode)
        {
            _stationCode = stationCode;
        }

        public override bool Execute()
        {
            Validations.Add(new CheckIfNullEmptySpace(_stationCode, FieldName));
            Validations.Add(new CheckLength(_stationCode, FieldName, 3));
            Validations.Add(new CheckIfStartsWithLetter(_stationCode, FieldName));

            return IsValid;
        }
    }
}
