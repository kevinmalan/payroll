using Payroll.MVC.Services.Contracts;
using System.Threading.Tasks;
using static Payroll.MVC.Services.TaxHelperService;

namespace Payroll.MVC.Services
{
    public class FlatValueTaxCalculator : ITaxRateCalculator
    {
        public async Task<decimal> CalculateTaxAmountAsync(decimal annualIncome)
        {
            ValidateAnnualIncome(annualIncome);
            // TODO: lookup rates from query service.
            if (annualIncome < 200000M)
            {
                return CalculateAmountPercentage(annualIncome, 5M);
            }

            return 10000M;
        }
    }
}