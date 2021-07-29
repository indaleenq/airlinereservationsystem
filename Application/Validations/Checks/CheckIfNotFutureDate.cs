using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations.Checks
{
    public class CheckIfNotFutureDate : ValidationBase<string>
    {
        public CheckIfNotFutureDate(string context, string fieldName)
           : base(context, fieldName)
        {

        }
        public override bool IsValid
        {
            get
            {
                var date = new DateTime();

                if (DateTime.TryParse(Context, out date))
                {
                    return DateTime.Parse(Context).Date < DateTime.Now.Date;
                }
                else
                {
                    return false;
                }

            }
        }

        public override string Message
        {
            get
            {
                return !IsValid ?
                       $"{_fieldName} should be a past date." :
                       string.Empty;
            }
        }
    }
}
