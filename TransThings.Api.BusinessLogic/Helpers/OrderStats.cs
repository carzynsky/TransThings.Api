using System;
using System.Collections.Generic;
using System.Text;

namespace TransThings.Api.BusinessLogic.Helpers
{
    public class OrderStats
    {
        public List<OrdersByMonth> OrdersByLastMonths { get; set; }
        public int OrdersLastMonth { get; set; }
        public int OrdersThisMonth { get; set; }
        public int TimelyDeliveriesRatio { get; set; }
        public string LastMonthComparerMessage { get; set; }

    }
    public struct OrdersByMonth
    {
        public OrdersByMonth(string month, int amountOfOrders)
        {
            Month = month;
            AmountOfOrders = amountOfOrders;
        }
        public string Month { get; set; }
        public int AmountOfOrders { get; set; }
    }
}
