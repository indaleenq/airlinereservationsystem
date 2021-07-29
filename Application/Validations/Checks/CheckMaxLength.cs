using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations.Checks
{
    public class CheckMaxLength : ValidationBase<string>
    {
        private int _maxLength;
        public CheckMaxLength(string context, string fieldName, int maxLength)
            : base(context, fieldName)
        {
            _maxLength = maxLength;
        }

        public override bool IsValid 
        {
            get
            {
                return Context.Length <= _maxLength;
            }
        }

        public override string Message
        {
            get
            {
                return $"{_fieldName} should be not more than {_maxLength} characters.";
            }
        }
    }
}
