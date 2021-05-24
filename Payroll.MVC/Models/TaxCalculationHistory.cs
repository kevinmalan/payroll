using Payroll.MVC.Models.Enums;
using System;

namespace Payroll.MVC.Models
{
    public class TaxCalculationHistory
    {
        public TaxCalculationHistory()
        {
            Id = Guid.NewGuid();
            CreatedOn = DateTimeOffset.UtcNow;
        }

        public Guid Id { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public decimal AnnualIncome { get; set; }
        public string PostalCode { get; set; }
        public decimal CalculatedTax { get; set; }
        public TaxType CalculationType { get; set; }
    }
}