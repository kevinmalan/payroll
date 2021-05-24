using Payroll.MVC.Dtos.Requests;
using System.Threading.Tasks;

namespace Payroll.MVC.Services.Contracts
{
    public interface ITaxCommandService
    {
        public Task CreateTaxCalculationHistoryAsync(TaxCalculatorHistoryRequest request);
    }
}