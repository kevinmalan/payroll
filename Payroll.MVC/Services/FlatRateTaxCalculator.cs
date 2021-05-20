using Payroll.MVC.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Payroll.MVC.Services
{
    public class FlatRateTaxCalculator : ITaxRateCalculator
    {
        public async Task<decimal> CalculateTaxAmountAsync(decimal annualIncome)
        {
            if (annualIncome < 0M)
            {
                throw new ArgumentException($"The provided annual income '{annualIncome}' should not be below 0.");
            }

            return annualIncome * (17.5M / 100M);
        }
    }
}