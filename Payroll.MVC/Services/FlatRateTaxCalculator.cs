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

            var queryService = new TaxQueryService();

            var flatRate = await queryService.GetFlatRateAsync(annualIncome);

            return CalculateAmountPercentage(annualIncome, flatRate);
        }
    }
}