using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Payroll.MVC.Models;
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

        public TaxCalculatorController(ILogger<TaxCalculatorController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}