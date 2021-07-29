using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Validations
{
    public class CheckIfLettersOnly : ValidationBase<string>
    {
        public CheckIfLettersOnly(string context, string fieldName) : base(context, fieldName)
        {

        }

        public override bool IsValid
        {
            get
            {
                if (!string.IsNullOrWhiteSpace(Context))
                {
                    var contextArray = Context.ToCharArray();

                    if (Regex.IsMatch(contextArray[0].ToString(), @"^[a-zA-Z]+$"))
                    {
                        if (Regex.IsMatch(Context, @"^[a-zA-Z]+$"))
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
                        if (Regex.IsMatch(contextArray[1].ToString(), @"^[a-zA-Z]+$"))
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
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
                       $"{_fieldName} should be all letters except if the first character is number." :
                       string.Empty;
            }
        }
    }
}
