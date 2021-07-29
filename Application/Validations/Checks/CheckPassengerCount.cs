using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations.Checks
{
    public class CheckPassengerCount : ValidationBase<List<PassengerModel>>
    {
        private int _maxPassengerCount;
        private int _minPassengerCount;
        public CheckPassengerCount(List<PassengerModel> context, string fieldName, int minPaxCount, int maxPaxCount)
            :base(context, fieldName)
        {
            _maxPassengerCount = maxPaxCount;
            _minPassengerCount = minPaxCount;
        }

        public override bool IsValid
        {
            get
            {
                return Context.Count >= _minPassengerCount
                    && Context.Count <= _maxPassengerCount;
            }
        }

        public override string Message
        {
            get
            {
                return !IsValid ?
                       $"Number of {_fieldName} should be {_minPassengerCount } to {_maxPassengerCount}." :
                       string.Empty;
            }
        }
    }
}
