using Payroll.MVC.Services.Contracts;
using System.Threading.Tasks;
using static Payroll.MVC.Services.TaxHelperService;

namespace Payroll.MVC.Services
{
    public class FlatRateTaxCalculator : ITaxRateCalculator
    {
        public async Task<decimal> CalculateTaxAmountAsync(decimal annualIncome)
        {
            ValidateAnnualIncome(annualIncome);
            // TODO: lookup rates from query service.
            return CalculateAmountPercentage(annualIncome, 17.5M);
        }
    }
}