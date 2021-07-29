using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Application
{
    public static class PNRGenerator
    {
        public static string Generate()
        {
            Random random = new Random();
            string firstCharOptions = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            var firstCharPNR = Enumerable.Repeat(firstCharOptions, 1) //literal 1
              .Select(s => s[random.Next(s.Length)]).ToArray();


            string alphanumOptions = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var alphaNums = Enumerable.Repeat(alphanumOptions, 5) //literal 6
              .Select(s => s[random.Next(s.Length)]).ToArray();

            var pnr = firstCharPNR.First().ToString();

            foreach (var alphaNum in alphaNums)
            {
                pnr = pnr + alphaNum.ToString();
            }

            return pnr.ToUpper();
        }
    }
}
