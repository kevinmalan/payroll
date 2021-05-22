using Payroll.MVC.Models.Enums;
using System.Threading.Tasks;

namespace Payroll.MVC.Services.Contracts
{
    public interface ITaxQueryService
    {
        public Task<TaxType> GetTaxCalculationTypeByPostalCodeAsync(string postalCode);
    }
}