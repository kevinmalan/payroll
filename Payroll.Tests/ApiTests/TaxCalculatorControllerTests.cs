using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Payroll.MVC.Common;
using Payroll.MVC.Controllers;
using Payroll.MVC.Dtos.Requests;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services;
using Payroll.MVC.Services.Contracts;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace Payroll.Tests.ApiTests
{
    [TestFixture]
    public class TaxCalculatorControllerTests
    {
        [Test]
        public async Task Get_WhenRequestIsValidTaxType_ShouldReturnCorrectTaxCalculator()
        {
            // Arrange
            var loggerMock = Substitute.For<ILogger<TaxCalculatorController>>();
            var controller = new TaxCalculatorController(loggerMock, TestHelper.GetTaxRateCalculatorFactory());
            var request = new TaxCalculationRequest
            {
                // Move to TestCase
                AnnualIncome = 500,
                TaxType = TaxType.FlatValue
            };

            // Act
            var response = await controller.Post(request);

            // Assert
            // TODO
        }
    }
}