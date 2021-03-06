using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.MVC.Models
{
    public class FlatValue
    {
        public FlatValue()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public decimal From { get; set; }
        public decimal To { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal AdditionalAmount { get; set; }
    }
}