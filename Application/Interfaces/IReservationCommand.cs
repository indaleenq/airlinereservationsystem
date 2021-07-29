using Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IReservationCommand
    {
        public List<string> Messages { get; }
        public string PNR { get; }
        bool CreateReservation(ReservationModel reservation);
        bool ValidateFlightDate(string flightDate);
        bool ValidateBirthDate(string birthDate);
    }
}
