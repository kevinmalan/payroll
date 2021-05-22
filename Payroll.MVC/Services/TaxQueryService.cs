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
        public async Task<TaxType> GetTaxCalculationTypeByPostalCodeAsync(string postalCode)
        {
            var calculationTypes = new List<PostalCodeCalculationTypeMap>
            {
                new PostalCodeCalculationTypeMap
                {
                    PostalCode = "7441",
                    CalculationType = TaxType.Progressive
                },
                new PostalCodeCalculationTypeMap
                {
                    PostalCode = "A100",
                    CalculationType = TaxType.FlatValue
                },
                new PostalCodeCalculationTypeMap
                {
                    PostalCode = "7000",
                    CalculationType = TaxType.FlatRate
                },
                new PostalCodeCalculationTypeMap
                {
                    PostalCode = "1000",
                    CalculationType = TaxType.Progressive
                },
            };

            var calculationType = calculationTypes
                .Where(x => x.PostalCode == postalCode)
                .Select(x => x.CalculationType)
                .FirstOrDefault();

            return calculationType;
        }

        public async Task<TaxRateLookupDto> GetFlatRateAsync(decimal annualIncome)
        {
            var flatRates = new List<FlatRate>
            {
                new FlatRate
                {
                    From = 0,
                    To = decimal.MaxValue,
                    TaxPercentage = 17.5M,
                    AdditionalAmount = 0
                }
            };

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
            var flatValueRates = new List<FlatValue>
            {
                new FlatValue
                {
                    From = 0,
                    To = 199999.9999999999M,
                    TaxPercentage = 5M,
                    AdditionalAmount = 0M
                },
                new FlatValue
                {
                    From = 200000M,
                    To = decimal.MaxValue,
                    TaxPercentage = 0M,
                    AdditionalAmount = 10000M
                }
            };

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