using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{
    public interface IValidation
    {
        bool IsValid { get; }

        string Message { get; }
    }
}
