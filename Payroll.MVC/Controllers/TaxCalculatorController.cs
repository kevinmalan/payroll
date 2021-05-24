using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payroll.MVC.Dtos.Requests;
using Payroll.MVC.Dtos.Responses;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Payroll.MVC.Controllers
{
    public class TaxCalculatorController : BaseController
    {
        private readonly ILogger<TaxCalculatorController> _logger;
        private readonly Func<TaxType, ITaxRateCalculator> _taxRateCalculatorFactory;
        private readonly ITaxQueryService _taxQueryService;
        private readonly ITaxCommandService _taxCommandService;

        public TaxCalculatorController(
            ILogger<TaxCalculatorController> logger,
            Func<TaxType, ITaxRateCalculator> taxRateCalculatorFactory,
            ITaxQueryService taxQueryService,
            ITaxCommandService taxCommandService)
        {
            _logger = logger;
            _taxRateCalculatorFactory = taxRateCalculatorFactory;
            _taxQueryService = taxQueryService;
            _taxCommandService = taxCommandService;
        }

        [HttpPost]
        public async Task<IActionResult> CalculateTaxAsync([FromBody] TaxCalculationRequest taxCalculationRequest)
        {
            var taxType = await _taxQueryService.GetTaxCalculationTypeByPostalCodeAsync(taxCalculationRequest.PostalCode);
            var calculator = _taxRateCalculatorFactory(taxType);
            var taxAmountPayable = await calculator.CalculateTaxAmountAsync(taxCalculationRequest.AnnualIncome);
            await _taxCommandService.CreateTaxCalculationHistoryAsync(new TaxCalculatorHistoryRequest
            {
                AnnualIncome = taxCalculationRequest.AnnualIncome,
                PostalCode = taxCalculationRequest.PostalCode,
                CalculatedTax = taxAmountPayable,
                CalculationType = taxType
            });

            var response = new TaxCalculationResponse
            {
                TaxCalculationType = taxType,
                TaxAmountPayable = taxAmountPayable
            };

            return OkApiResponse(response);
        }
    }
}