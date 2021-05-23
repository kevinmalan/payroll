using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Payroll.MVC.Controllers;
using Payroll.MVC.Dtos.Requests;
using Payroll.MVC.Models.Enums;
using Shouldly;
using System.Threading.Tasks;

namespace Payroll.Tests.ControllerTests
{
    [TestFixture]
    public class TaxCalculatorControllerTests
    {
        [TestCase(TaxType.FlatRate, 500)]
        [TestCase(TaxType.FlatValue, 1000)]
        [TestCase(TaxType.Progressive, 1500)]
        public async Task Get_WhenRequestIsValidTaxType_ShouldReturnCorrectTaxCalculator(TaxType taxType, decimal annualIncome)
        {
            // Arrange
            var loggerMock = Substitute.For<ILogger<TaxCalculatorController>>();
            var factory = TestHelper.GetTaxRateCalculatorFactorySubstitude(annualIncome);
            var controller = new TaxCalculatorController(loggerMock, factory);
            var request = new TaxCalculationRequest
            {
                TaxType = taxType,
                AnnualIncome = annualIncome,
            };

            // Act
            var response = await controller.Post(request);

            // Assert
            var okResult = response as OkObjectResult;
            factory.Received(1)(Arg.Is<TaxType>(value => value == taxType));
            okResult.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(200);
            okResult.Value.ShouldBeOfType<decimal>();
            okResult.Value.ShouldBe(annualIncome);
        }
    }
}