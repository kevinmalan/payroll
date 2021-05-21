using Payroll.MVC.Models;
using Payroll.MVC.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.MVC.Services
{
    public class ProgressiveTaxCalculator : ITaxRateCalculator
    {
        public async Task<decimal> CalculateTaxAmountAsync(decimal annualIncome)
        {
            if (annualIncome < 0M)
            {
                throw new ArgumentException($"The provided annual income '{annualIncome}' should not be below 0.");
            }

            var progressiveRates = new List<ProgressiveRate>
            {
                new ProgressiveRate
                {
                    RatePercentage = 10M,
                    From = 0M,
                    To = 8350M,
                    AdditionalAmount = 0M
                },
                new ProgressiveRate
                {
                    RatePercentage = 15M,
                    From = 8351M,
                    To = 33950M,
                    AdditionalAmount =  0M
                },
                new ProgressiveRate
                {
                    RatePercentage = 25M,
                    From = 33951M,
                    To = 82250M,
                    AdditionalAmount =  0M
                },
                new ProgressiveRate
                {
                    RatePercentage = 28M,
                    From = 82251M,
                    To = 171550M ,
                    AdditionalAmount =   0M
                },
                new ProgressiveRate
                {
                    RatePercentage = 33M,
                    From = 171551M,
                    To = 372950M,
                    AdditionalAmount =   0M
                },
                new ProgressiveRate
                {
                    RatePercentage = 35M,
                    From = 372951M,
                    To = decimal.MaxValue,
                    AdditionalAmount =   0M
                }
            };

            decimal taxPayable = 0M;
            decimal annualIncomeNotTaxed = annualIncome;

            foreach (var rate in progressiveRates.OrderBy(x => x.RatePercentage))
            {
                if (annualIncomeNotTaxed == 0M) break;

                var amountTaxbale = Math.Min(annualIncomeNotTaxed, rate.To);
                taxPayable += amountTaxbale * (rate.RatePercentage / 100M);
                annualIncomeNotTaxed -= amountTaxbale;
            }

            return taxPayable;
        }
    }
}