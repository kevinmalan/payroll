using Payroll.MVC.Services.Contracts;
using System.Threading.Tasks;
using static Payroll.MVC.Services.TaxHelperService;

namespace Payroll.MVC.Services
{
    public class FlatRateTaxCalculator : ITaxRateCalculator
    {
        private readonly ITaxQueryService _taxQueryService;

        public FlatRateTaxCalculator()
        {
        }

        public FlatRateTaxCalculator(ITaxQueryService taxQueryService)
        {
            _taxQueryService = taxQueryService;
        }

        public async Task<decimal> CalculateTaxAmountAsync(decimal annualIncome)
        {
            ValidateAnnualIncome(annualIncome);

            var flatRate = await _taxQueryService.GetFlatRateAsync(annualIncome);

            return CalculateAmountPercentage(annualIncome, flatRate);
        }
    }
}