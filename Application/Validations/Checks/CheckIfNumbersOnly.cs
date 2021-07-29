using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Validations
{
    public class CheckIfNumbersOnly : ValidationBase<string>
    {
        public CheckIfNumbersOnly(string context, string fieldName) : base(context, fieldName)
        {

        }

        public override bool IsValid
        {
            get
            {
                return Regex.IsMatch(Context, @"^\d+$");
            }
        }

        public override string Message
        {
            get
            {
                return !IsValid ?
                       $"{_fieldName} should be a number." :
                       string.Empty;
            }
        }
    }
}
