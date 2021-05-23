using Payroll.MVC.Models.Enums;

namespace Payroll.MVC.Dtos.Requests
{
    public class TaxCalculationRequest
    {
        public string PostalCode { get; set; }
        public decimal AnnualIncome { get; set; }
    }
}