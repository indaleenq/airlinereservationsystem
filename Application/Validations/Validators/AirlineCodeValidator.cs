using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{
    public class AirlineCodeValidator : Validator
    {
        public string FieldName { get { return "Airline Code"; } }
        private string _airlineCode;

        public AirlineCodeValidator(string airlineCode)
        {
            _airlineCode = airlineCode;
        }

        public override bool Execute()
        {
            Validations.Add(new CheckIfNullEmptySpace(_airlineCode, FieldName));
            Validations.Add(new CheckLength(_airlineCode, FieldName, 2));
            Validations.Add(new CheckIfLettersOnly(_airlineCode, FieldName));

            return IsValid;
        }
    }
}
