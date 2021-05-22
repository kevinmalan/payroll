using Payroll.MVC.Models.Enums;

namespace Payroll.MVC.Models
{
    public class PostalCodeCalculationTypeMap
    {
        public string PostalCode { get; set; }
        public TaxType CalculationType { get; set; }
    }
}