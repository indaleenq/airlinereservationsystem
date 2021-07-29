using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations.Checks
{
    public class CheckIfValidDateFormat : ValidationBase<string>
    {
        public CheckIfValidDateFormat(string context, string fieldName) : base(context, fieldName)
        {

        }
        public override bool IsValid
        {
            get
            {
                var date = new DateTime();

                return DateTime.TryParse(Context, out date);
            }
        }

        public override string Message
        {
            get
            {
                return !IsValid ?
                       $"{_fieldName} is not a valid format. Should be mm/dd/yyyy." :
                       string.Empty;
            }
        }
    }
}
