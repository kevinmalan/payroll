using Microsoft.EntityFrameworkCore;
using Payroll.MVC.Dtos;
using Payroll.MVC.Models;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.MVC.Services
{
    public class TaxQueryService : ITaxQueryService
    {
        private readonly DataContext _dataContext;

        public TaxQueryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<TaxType> GetTaxCalculationTypeByPostalCodeAsync(string postalCode)
        {
            var calculationTypes = await _dataContext.PostalCodeCalculationTypeMaps.ToListAsync();

            var calculationType = calculationTypes
                .Where(x => x.PostalCode == postalCode)
                .Select(x => x.CalculationType)
                .FirstOrDefault();

            return calculationType;
        }

        public async Task<TaxRateLookupDto> GetFlatRateAsync(decimal annualIncome)
        {
            var flatRates = await _dataContext.FlatRates.ToListAsync();

            var flatRatePercentage = flatRates
                .Where(x => x.From <= annualIncome && x.To >= annualIncome)
                .Select(x => new TaxRateLookupDto
                {
                    RateFrom = x.From,
                    RateTo = x.To,
                    TaxPercentage = x.TaxPercentage,
                    AdditionalAmount = x.AdditionalAmount
                })
                .FirstOrDefault();

            return flatRatePercentage;
        }

        public async Task<TaxRateLookupDto> GetFlatValueRateAsync(decimal annualIncome)
        {
            var flatValueRates = await _dataContext.FlatValues.ToListAsync();

            var flatValue = flatValueRates
                .Where(x => x.From <= annualIncome && x.To >= annualIncome)
                .Select(x => new TaxRateLookupDto
                {
                    RateFrom = x.From,
                    RateTo = x.To,
                    TaxPercentage = x.TaxPercentage,
                    AdditionalAmount = x.AdditionalAmount
                })
                .FirstOrDefault();

            return flatValue;
        }

        public async Task<IEnumerable<TaxRateLookupDto>> GetProgressiveRatesAsync()
        {
            var progressiveRates = await _dataContext.ProgressiveRates.ToListAsync();

            var taxRatesLookup = new List<TaxRateLookupDto>();
            taxRatesLookup.AddRange(progressiveRates.Select(x => new TaxRateLookupDto
            {
                RateFrom = x.From,
                RateTo = x.To,
                TaxPercentage = x.RatePercentage,
                AdditionalAmount = x.AdditionalAmount
            }));

            return taxRatesLookup;
        }
    }
}