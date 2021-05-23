using Payroll.MVC.Models.Enums;
using System;

namespace Payroll.MVC.Models
{
    public class PostalCodeCalculationTypeMap
    {
        public PostalCodeCalculationTypeMap()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string PostalCode { get; set; }
        public TaxType CalculationType { get; set; }
    }
}