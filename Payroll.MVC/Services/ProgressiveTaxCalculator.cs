using Payroll.MVC.Models;
using Payroll.MVC.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Payroll.MVC.Services.TaxHelperService;

namespace Payroll.MVC.Services
{
    public class ProgressiveTaxCalculator : ITaxRateCalculator
    {
        public async Task<decimal> CalculateTaxAmountAsync(decimal annualIncome)
        {
            ValidateAnnualIncome(annualIncome);

            var queryService = new TaxQueryService();
            var progressiveRatesLookup = await queryService.GetProgressiveRatesAsync();
            var taxPayable = 0M;
            var annualIncomeNotTaxed = annualIncome;

            foreach (var progressiveRate in progressiveRatesLookup.OrderBy(x => x.TaxPercentage))
            {
                if (annualIncomeNotTaxed == 0M) break;

                var amountTaxbale = Math.Min(annualIncomeNotTaxed, progressiveRate.RateTo);
                taxPayable += CalculateAmountPercentage(amountTaxbale, progressiveRate);
                annualIncomeNotTaxed -= amountTaxbale;
            }

            return taxPayable;
        }
    }
}