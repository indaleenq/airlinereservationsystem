using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Validations.Checks
{
    public class CheckIfStartsWithLetter : ValidationBase<string>
    {
        public CheckIfStartsWithLetter(string context, string fieldName) : base(context, fieldName)
        {

        }

        public override bool IsValid 
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Context))
                {
                    var contextArray = Context.ToCharArray();

                    //refactor
                    if (Regex.IsMatch(contextArray[0].ToString(), @"^[a-zA-Z]+$"))
                    {
                        if (Regex.IsMatch(Context, @"^[a-zA-Z0-9]*$"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
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
                       $"{_fieldName} should be an alphanumeric that starts with a letter." :
                       string.Empty;
            }
        }
    }
}
