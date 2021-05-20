using Payroll.MVC.Services.Contracts;
using System.Threading.Tasks;

namespace Payroll.MVC.Services
{
    public class TaxRateCalculator : ITaxRateCalculator
    {
        public async Task<decimal> CalculateTaxAmountAsync(decimal annualIncome)
        {
            return 35000M;
        }
    }
}