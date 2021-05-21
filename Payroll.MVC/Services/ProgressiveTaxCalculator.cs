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
                    To = 33950,
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
                    From = 82251,
                    To = 171550 ,
                    AdditionalAmount =   0M
                },
                new ProgressiveRate
                {
                    RatePercentage = 33,
                    From = 171551,
                    To = 372950 ,
                    AdditionalAmount =   0M
                },
                new ProgressiveRate
                {
                    RatePercentage = 35,
                    From = 372951,
                    To = decimal.MaxValue,
                    AdditionalAmount =   0M
                }
            };

            decimal taxPayable = 0M;
            decimal annualIncomeNotTaxed = annualIncome;

            foreach (var rate in progressiveRates.OrderBy(x => x.RatePercentage))
            {
                if (annualIncomeNotTaxed == 0) break;

                var amountTaxbale = Math.Min(annualIncomeNotTaxed, rate.To);
                taxPayable += amountTaxbale * (rate.RatePercentage / 100M);

                annualIncomeNotTaxed -= amountTaxbale;
            }

            return taxPayable;
        }
    }
}