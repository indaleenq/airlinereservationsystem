using Application.Validations.Checks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations.Validators
{
    public class PassengerBirthDateValidator : Validator
    {
        public string FieldName { get { return "Birth Date"; } }
        private string _birthDate;
        public PassengerBirthDateValidator(string birthDate)
        {
            _birthDate = birthDate;
        }

        public override bool Execute()
        {
            Validations.Add(new CheckIfValidDateFormat(_birthDate, FieldName));
            Validations.Add(new CheckIfNotFutureDate(_birthDate, FieldName));
            return IsValid;
        }
    }
}
