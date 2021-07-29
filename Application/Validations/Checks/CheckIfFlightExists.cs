using Application.Models;
using Application.Queries;
using Data.Interfaces;

namespace Application.Validations
{
    public class CheckIfFlightExists : ValidationBase<FlightModel>
    {
        private FlightQuery _flightQuery;

        public CheckIfFlightExists(IDataService dataService, FlightModel context, string fieldName) : base(context, fieldName)
        {
            _flightQuery = new FlightQuery(dataService);
        }

        public override bool IsValid
        {
            get
            {
                var flight = _flightQuery.GetFlight(Context.AirlineCode + " " + Context.FlightNumber,
                               Context.DepartureStationCode, Context.ArrivalStationCode);
                return flight == null ? true : flight.FlightNumber == null ? true : false;
            }
        }

        public override string Message
        {
            get
            {
                return !IsValid ?
                       $"{_fieldName} already exists." :
                       $"{_fieldName} does not exists.";
            }
        }
    }
}
