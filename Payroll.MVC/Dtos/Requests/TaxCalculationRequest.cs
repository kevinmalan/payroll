using Payroll.MVC.Models.Enums;

namespace Payroll.MVC.Dtos.Requests
{
    public class TaxCalculationRequest
    {
        public TaxType TaxType { get; set; }
        public decimal AnnualIncome { get; set; }
    }
}