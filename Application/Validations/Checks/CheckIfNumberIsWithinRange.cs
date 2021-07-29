using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{
    public class CheckIfNumberIsWithinRange : ValidationBase<string>
    {
        private int _min;
        private int _max;
        public CheckIfNumberIsWithinRange(string context, string fieldName, int min, int max) 
            : base(context, fieldName)
        {
            _min = min;
            _max = max;
        }

        public override bool IsValid
        {
            get
            {
                int contextNumber;
                return int.TryParse(Context, out contextNumber) ?
                    (contextNumber >= _min && contextNumber <= _max) : false;
            }
        }

        public override string Message
        {
            get
            {
                return !IsValid ?
                       $"{_fieldName} should be from {_min} to {_max}." :
                       string.Empty;
            }
        }
    }
}
