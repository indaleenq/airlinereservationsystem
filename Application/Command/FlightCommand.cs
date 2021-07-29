using Application.Interfaces;
using Application.Models;
using Application.Validations;
using Data;
using Data.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Command
{
    public class FlightCommand : IFlightCommand
    {
        private IDataService _dataService;
        private List<string> CommandMessages = new List<string>();
      
        public FlightCommand(IDataService dataService)
        {
            _dataService = dataService;
        }

        public List<string> Messages { get { return CommandMessages; } }

        public bool CreateFlight(FlightModel flightModel)
        {
            try
            {
                var flightValidator = new FlightValidator(flightModel, _dataService);

                if (flightValidator.Execute())
                {
                    var flight = FlightFactory.Create(flightModel);
                    _dataService.AddFlight(flight);
                    return true;
                }
                else
                {
                    CommandMessages.Clear();
                    CommandMessages.AddRange(flightValidator.AllErrorMessages);
                    return false;
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
