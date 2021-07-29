using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{
    public class CheckIfNullEmptySpace : ValidationBase<string>
    {
        public CheckIfNullEmptySpace(string context, string fieldName) : base(context, fieldName)
        {
        }

        public override bool IsValid
        {
            get
            {
                return !string.IsNullOrEmpty(Context) && !string.IsNullOrWhiteSpace(Context);
            }
        }

        public override string Message
        {
            get
            {
                return !IsValid ? 
                       $"{_fieldName} is required. It cannot be null, empty or whitespace." :
                       string.Empty;
            }
        }
    }
}
