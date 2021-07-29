using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations
{
    public abstract class ValidationBase<T> : IValidation where T : class
    {
        protected T Context { get; private set; }
        protected string _fieldName { get; private set; }

        public ValidationBase(T context, string fieldName)
        {
            Context = context;
            _fieldName = fieldName;
        }

        public abstract bool IsValid { get; }

        public abstract string Message { get; }
    }
}
