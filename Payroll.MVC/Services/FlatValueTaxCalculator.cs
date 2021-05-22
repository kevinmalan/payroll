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

            var queryService = new TaxQueryService();
            var flatValueRate = await queryService.GetFlatValueRateAsync(annualIncome);

            return CalculateAmountPercentage(annualIncome, flatValueRate);
        }
    }
}