using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FAS.WebUI.Models
{
    public class ChartModel<T>
    {
        public T Value { get; set; }
        public string Category { get; set; }
    }

    public class StackChartModel
    {
        public string Category { get; set; }
        public double Incomes { get; set; }
        public double Expenses { get; set; }
    }
}