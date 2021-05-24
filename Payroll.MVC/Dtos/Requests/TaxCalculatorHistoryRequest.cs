using Payroll.MVC.Models.Enums;

namespace Payroll.MVC.Dtos.Requests
{
    public class TaxCalculatorHistoryRequest
    {
        public decimal AnnualIncome { get; set; }
        public string PostalCode { get; set; }
        public decimal CalculatedTax { get; set; }
        public TaxType CalculationType { get; set; }
    }
}