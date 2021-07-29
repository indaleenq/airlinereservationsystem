using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{
    public class CheckLength : ValidationBase<string>
    {
        private int _requiredLength;

        public CheckLength(string context, string fieldName, int requiredLength) : base(context, fieldName)
        {
            _requiredLength = requiredLength;
        }

        public override bool IsValid
        {
            get
            {
                return Context.Length == _requiredLength;
            }
        }

        public override string Message
        {
            get
            {
                return $"{_fieldName} should be {_requiredLength} characters.";
            }
        }
    }
}
