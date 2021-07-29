using Application.Validations.Checks;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Validations.Validators
{
    public class PNRValidator : Validator
    {
        public string FieldName { get { return "PNR"; } }
        private string _pnr;
        private IDataService _dataService;
        public List<string> AllErrorMessages { get; private set; }

        public PNRValidator(IDataService dataService, string pnr)
        {
            _dataService = dataService;
            _pnr = pnr;
            AllErrorMessages = new List<string>();
        }

        public override bool Execute()
        {
            Validations.Add(new CheckIfStartsWithLetter(_pnr, FieldName));
            Validations.Add(new CheckLength(_pnr, FieldName, 6));
            Validations.Add(new CheckIfPNRExists(_dataService, _pnr, FieldName));

            if (IsValid)
            {
                return IsValid;
            }
            else
            {
                AllErrorMessages.AddRange(ErrorMessages);
                return false;
            }
        }
    }
}
