using Microsoft.AspNetCore.Mvc;
using Payroll.MVC.Services.Contracts;
using System.Threading.Tasks;

namespace Payroll.MVC.Controllers
{
    public class PostalCodeController : BaseController
    {
        private readonly ITaxQueryService _taxQueryService;

        public PostalCodeController(ITaxQueryService taxQueryService)
        {
            _taxQueryService = taxQueryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPostalCodesAsync()
        {
            var postalCodes = await _taxQueryService.GetPostalCodesAsync();

            return OkApiResponse(postalCodes);
        }
    }
}