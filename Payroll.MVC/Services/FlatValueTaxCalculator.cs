using Payroll.MVC.Services.Contracts;
using System.Threading.Tasks;
using static Payroll.MVC.Services.TaxHelperService;

namespace Payroll.MVC.Services
{
    public class FlatValueTaxCalculator : ITaxRateCalculator
    {
        private readonly ITaxQueryService _taxQueryService;

        public FlatValueTaxCalculator()
        {
        }

        public FlatValueTaxCalculator(ITaxQueryService taxQueryService)
        {
            _taxQueryService = taxQueryService;
        }

        public async Task<decimal> CalculateTaxAmountAsync(decimal annualIncome)
        {
            ValidateAnnualIncome(annualIncome);

            var flatValueRate = await _taxQueryService.GetFlatValueRateAsync(annualIncome);

            return CalculateAmountPercentage(annualIncome, flatValueRate);
        }
    }
}