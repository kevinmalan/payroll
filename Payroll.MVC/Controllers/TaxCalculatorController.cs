using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payroll.MVC.Dtos.Requests;
using Payroll.MVC.Models;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.MVC.Controllers
{
    public class TaxCalculatorController : Controller
    {
        private readonly ILogger<TaxCalculatorController> _logger;
        private readonly Func<TaxType, ITaxRateCalculator> _taxRateCalculatorFactory;

        public TaxCalculatorController(
            ILogger<TaxCalculatorController> logger,
            Func<TaxType, ITaxRateCalculator> taxRateCalculatorFactory)
        {
            _logger = logger;
            _taxRateCalculatorFactory = taxRateCalculatorFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TaxCalculationRequest taxCalculationRequest)
        {
            var calculator = _taxRateCalculatorFactory(taxCalculationRequest.TaxType);
            var result = await calculator.CalculateTaxAmountAsync(taxCalculationRequest.AnnualIncome);

            return Ok(result);
        }
    }
}