using Microsoft.AspNetCore.Mvc;
using Payroll.MVC.Services.Contracts;
using System.Threading.Tasks;

namespace Payroll.MVC.Controllers
{
    public class TaxCalculatorHistoryController : BaseController
    {
        private readonly ITaxQueryService _taxQueryService;

        public TaxCalculatorHistoryController(ITaxQueryService taxQueryService)
        {
            _taxQueryService = taxQueryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetTaxTransactionHistoryAsync()
        {
            var taxTransactionHistory = await _taxQueryService.GetTaxCalculatorHistoryAsync();

            return OkApiResponse(taxTransactionHistory);
        }
    }
}