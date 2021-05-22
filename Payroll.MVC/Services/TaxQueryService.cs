using Payroll.MVC.Models;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.MVC.Services
{
    public class TaxQueryService : ITaxQueryService
    {
        public async Task<TaxType> GetTaxCalculationTypeByPostalCodeAsync(string postalCode)
        {
            var calculationTypes = new List<PostalCodeCalculationTypeMap>
            {
                new PostalCodeCalculationTypeMap
                {
                    PostalCode = "7441",
                    CalculationType = TaxType.Progressive
                },
                new PostalCodeCalculationTypeMap
                {
                    PostalCode = "A100",
                    CalculationType = TaxType.FlatValue
                },
                new PostalCodeCalculationTypeMap
                {
                    PostalCode = "7000",
                    CalculationType = TaxType.FlatRate
                },
                new PostalCodeCalculationTypeMap
                {
                    PostalCode = "1000",
                    CalculationType = TaxType.Progressive
                },
            };

            var calculationType = calculationTypes
                .Where(x => x.PostalCode == postalCode)
                .Select(x => x.CalculationType)
                .FirstOrDefault();

            return calculationType;
        }
    }
}