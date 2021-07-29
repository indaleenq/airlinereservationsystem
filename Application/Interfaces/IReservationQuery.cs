using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IReservationQuery
    {
        List<ReservationModel> GetReservations();
        ReservationModel GetReservation(string pnr);
    }
}
