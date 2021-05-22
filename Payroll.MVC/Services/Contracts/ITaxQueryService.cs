using Payroll.MVC.Dtos;
using Payroll.MVC.Models.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payroll.MVC.Services.Contracts
{
    public interface ITaxQueryService
    {
        public Task<TaxType> GetTaxCalculationTypeByPostalCodeAsync(string postalCode);

        public Task<TaxRateLookupDto> GetFlatRateAsync(decimal annualIncome);

        public Task<TaxRateLookupDto> GetFlatValueRateAsync(decimal annualIncome);

        Task<IEnumerable<TaxRateLookupDto>> GetProgressiveRatesAsync();
    }
}