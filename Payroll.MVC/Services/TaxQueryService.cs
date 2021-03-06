using Microsoft.EntityFrameworkCore;
using Payroll.MVC.Dtos;
using Payroll.MVC.Dtos.Responses;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services.Contracts;
using System;
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
            postalCode = postalCode.Trim();

            var calculationTypes = await _dataContext.PostalCodeCalculationTypeMap.ToListAsync();

            var postalCodeCalculationMap = calculationTypes
                .Where(x => x.PostalCode == postalCode)
                .Select(x => new { x.CalculationType })
                .FirstOrDefault();

            if (postalCodeCalculationMap is null)
            {
                throw new ArgumentException($"No tax calculation type found for postal code '{postalCode}'");
            }

            return postalCodeCalculationMap.CalculationType;
        }

        public async Task<IEnumerable<PostalCodeResponse>> GetPostalCodesAsync()
        {
            return await _dataContext.PostalCodeCalculationTypeMap
                .Select(x => new PostalCodeResponse
                {
                    PostalCode = x.PostalCode
                })
                .ToListAsync();
        }

        public async Task<TaxRateLookupDto> GetFlatRateAsync(decimal annualIncome)
        {
            var flatRates = await _dataContext.FlatRate.ToListAsync();

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
            var flatValueRates = await _dataContext.FlatValue.ToListAsync();

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
            var progressiveRates = await _dataContext.ProgressiveRate.ToListAsync();

            var taxRatesLookup = new List<TaxRateLookupDto>();
            taxRatesLookup.AddRange(progressiveRates.Select(x => new TaxRateLookupDto
            {
                RateFrom = x.From,
                RateTo = x.To,
                TaxPercentage = x.TaxPercentage,
                AdditionalAmount = x.AdditionalAmount
            }));

            return taxRatesLookup;
        }

        public async Task<IEnumerable<TaxCalculatorHistoryResponse>> GetTaxCalculatorHistoryAsync()
        {
            return await _dataContext.TaxCalculationHistory
                .OrderByDescending(x => x.CreatedOn)
                .Select(x => new TaxCalculatorHistoryResponse
                {
                    AnnualIncome = x.AnnualIncome,
                    CalculatedTax = x.CalculatedTax,
                    CalculationType = x.CalculationType,
                    PostalCode = x.PostalCode,
                    CreatedOnDateString = x.CreatedOn.ToString("dd-MMMM-yyy HH:mm:ss")
                })
                .ToListAsync();
        }
    }
}