using Application.Models;
using Application.Validations.Checks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{
    public class PassengerValidator : Validator
    {
        public string FieldName { get { return "Passenger"; } }
        private List<PassengerModel> _passengers;

        public PassengerValidator(List<PassengerModel> passengers)
        {
            _passengers = passengers;
        }

        public override bool Execute()
        {
            Validations.Add(new CheckPassengerCount(_passengers, FieldName, 1, 5));

            foreach (var passenger in _passengers)
            {
                Validations.Add(new CheckIfNullEmptySpace(passenger.FirstName, FieldName + " First Name"));
                Validations.Add(new CheckIfNullEmptySpace(passenger.LastName, FieldName + " Last Name"));
                Validations.Add(new CheckMaxLength(passenger.FirstName, FieldName + " First Name", 20));
                Validations.Add(new CheckMaxLength(passenger.LastName, FieldName + " Last Name", 20));
            }

            return IsValid;
        }
    }
}
