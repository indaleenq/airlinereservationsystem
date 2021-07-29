using System;

namespace Application.Validations
{
    public class CheckIfNotPastDate : ValidationBase<string>
    {
        public CheckIfNotPastDate(string context, string fieldName)
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
                    return DateTime.Parse(Context).Date >= DateTime.Now.Date;
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
                       $"{_fieldName} should be a future date." :
                       string.Empty;
            }
        }
    }
}
