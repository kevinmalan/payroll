using System.Threading.Tasks;

namespace Payroll.MVC.Services.Contracts
{
    public interface ITaxRateCalculator
    {
        Task<decimal> CalculateTaxAmountAsync(decimal annualIncome);
    }
}