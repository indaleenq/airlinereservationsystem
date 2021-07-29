using Application.Models;
using Application.Queries;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations.Checks
{
    public class CheckIfPNRExists : ValidationBase<string>
    {
        private ReservationQuery _reservationQuery;

        public CheckIfPNRExists(IDataService dataservice, string context, string fieldName)
            : base(context, fieldName)
        {
            _reservationQuery = new ReservationQuery(dataservice);
        }

        public override bool IsValid
        {
            get
            {
                var reservation = _reservationQuery.GetReservation(Context);
                return reservation == null ? true : reservation.PNR == null ? true : false;
            }
        }

        public override string Message
        {
            get
            {
                return !IsValid ?
                       $"{_fieldName} already exists." :
                       string.Empty;
            }
        }
    }
}
