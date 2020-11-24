using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Helpers
{
    public class TransitStats
    {
        public List<TransitsByMonth> TransitsByLastMonths { get; set; }

    }
    public struct TransitsByMonth
    {
        public TransitsByMonth(string month, int amountOfTransits)
        {
            Month = month;
            AmountOfTransits = amountOfTransits;
        }
        public string Month { get; set; }
        public int AmountOfTransits { get; set; }
    }
}
