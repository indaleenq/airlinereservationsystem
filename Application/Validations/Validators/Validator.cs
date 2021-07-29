using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Validations
{
    public abstract class Validator
    {
        public List<IValidation> Validations = new List<IValidation>();

        public List<string> ErrorMessages 
        {
            get
            {
                var messages = new List<string>();
                foreach (var validation in Validations)
                {
                    if (!validation.IsValid)
                    {
                        messages.Add(validation.Message);
                    }
                }
                return messages;
            }
        }

        internal bool IsValid
        {
            get
            {
                var t = Validations.All(x => x.IsValid);
                return t;
            }
        }

        public abstract bool Execute();
    }
}
