using Payroll.MVC.Models.Enums;

namespace Payroll.MVC.Dtos.Responses
{
    public class TaxCalculatorHistoryResponse
    {
        public decimal AnnualIncome { get; set; }
        public string PostalCode { get; set; }
        public decimal CalculatedTax { get; set; }
        public TaxType CalculationType { get; set; }
        public string CreatedOnDateString { get; set; }
    }
}