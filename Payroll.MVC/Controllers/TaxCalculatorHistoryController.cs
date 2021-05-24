using Microsoft.AspNetCore.Mvc;
using Payroll.MVC.Services.Contracts;
using System.Threading.Tasks;

namespace Payroll.MVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TaxCalculatorHistoryController : Controller
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

            return Ok(taxTransactionHistory);
        }
    }
}