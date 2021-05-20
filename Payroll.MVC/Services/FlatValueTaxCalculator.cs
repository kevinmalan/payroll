using Payroll.MVC.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.MVC.Services
{
    public class FlatValueTaxCalculator : ITaxRateCalculator
    {
        public async Task<decimal> CalculateTaxAmountAsync(decimal annualIncome)
        {
            if (annualIncome < 0M)
            {
                throw new ArgumentException($"The provided annual income '{annualIncome}' should not be below 0.");
            }

            if (annualIncome < 200000M)
            {
                return annualIncome * (5M / 100M);
            }

            return 10000M;
        }
    }
}