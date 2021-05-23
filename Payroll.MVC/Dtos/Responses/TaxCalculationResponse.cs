using Payroll.MVC.Models.Enums;

namespace Payroll.MVC.Dtos.Responses
{
    public class TaxCalculationResponse
    {
        public TaxType TaxCalculationType { get; set; }
        public decimal TaxAmountPayable { get; set; }
    }
}