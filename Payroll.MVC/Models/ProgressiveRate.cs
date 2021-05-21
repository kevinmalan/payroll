using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.MVC.Models
{
    public class ProgressiveRate
    {
        public decimal From { get; set; }
        public decimal To { get; set; }
        public decimal RatePercentage { get; set; }
        public decimal AdditionalAmount { get; set; }
    }
}