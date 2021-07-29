using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations.Checks
{
    public class CheckIfValidTimeFormat : ValidationBase<string>
    {

        public CheckIfValidTimeFormat(string context, string fieldName) : base(context, fieldName)
        {

        }
        public override bool IsValid
        {
            get
            {
                var time = new DateTime();

                return DateTime.TryParse(Context, out time);
            }
        }

        public override string Message 
        { 
            get
            {
                return !IsValid ?
                       $"{_fieldName} is not a valid format. Should be hh:mm." :
                       string.Empty;
            }
        }
    }
}
