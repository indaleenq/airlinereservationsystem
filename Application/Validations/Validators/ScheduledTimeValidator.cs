using Application.Validations.Checks;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations.Validators
{
    public class ScheduledTimeValidator : Validator
    {
        public string FieldName { get { return "Time"; } }
        private string _time;

        public ScheduledTimeValidator(string time)
        {
            _time = time;
        }

        public override bool Execute()
        {
            Validations.Add(new CheckIfValidTimeFormat(_time, FieldName));
            return IsValid;
        }
    }
}
