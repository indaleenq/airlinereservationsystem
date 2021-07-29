using Application.Interfaces;
using Application.Models;
using Application.Validations.Validators;
using Data.Interfaces;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application.Command
{
    public class ReservationCommand : IReservationCommand
    {
        private readonly IDataService _dataService;
        private List<string> CommandMessages = new List<string>();
        private string _pnrGenerated;
        public ReservationCommand(IDataService dataService)
        {
            _dataService = dataService;
        }
        public List<string> Messages { get { return CommandMessages; } }

        public string PNR
        {
            get
            {
                return _pnrGenerated;
            }
        }

        public bool CreateReservation(ReservationModel reservationModel)
        {
            try
            {
                var reservationValidator = new ReservationValidator(reservationModel, _dataService);

                if (reservationValidator.Execute())
                {
                    _pnrGenerated = PNRGenerator.Generate();
                    var pnrValidator = new PNRValidator(_dataService, _pnrGenerated);
                    var isPNRValid = pnrValidator.Execute();

                    if (isPNRValid)
                    {
                        _dataService.AddReservation(new Reservation()
                        {
                            PNR = _pnrGenerated,
                            Flight = FlightFactory.Create(reservationModel.Flight),
                            FlightDate = reservationModel.FlightDate.Date,
                            Passengers = PassengerFactory.CreateList(reservationModel.Passengers)
                        });
                        return true;
                    }
                    else
                    {
                        CommandMessages.Clear();
                        CommandMessages.AddRange(pnrValidator.AllErrorMessages);
                        return false;
                    }
                }
                else
                {
                    CommandMessages.Clear();
                    CommandMessages.AddRange(reservationValidator.AllErrorMessages);
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public bool ValidateFlightDate(string flightDate)
        {
            try
            {
                var flightDateValidator = new FlightDateValidator(flightDate);

                if (flightDateValidator.Execute())
                {
                    return true;
                }
                else
                {
                    CommandMessages.Clear();
                    CommandMessages.AddRange(flightDateValidator.ErrorMessages);
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        public bool ValidateBirthDate(string birthDate)
        {
            try
            {
                var _paxBdayValidator = new PassengerBirthDateValidator(birthDate);

                if (_paxBdayValidator.Execute())
                {
                    return true;
                }
                else
                {
                    CommandMessages.Clear();
                    CommandMessages.AddRange(_paxBdayValidator.ErrorMessages);
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
    }
}
