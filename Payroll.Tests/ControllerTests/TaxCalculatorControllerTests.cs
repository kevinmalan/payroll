using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Payroll.MVC.Controllers;
using Payroll.MVC.Dtos.Requests;
using Payroll.MVC.Dtos.Responses;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services.Contracts;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Payroll.Tests.ControllerTests
{
    [TestFixture]
    public class TaxCalculatorControllerTests : BaseTest
    {
        [TestCase(TaxType.FlatRate, 500, "7000")]
        [TestCase(TaxType.FlatValue, 1000, "A100")]
        [TestCase(TaxType.Progressive, 1500, "7441")]
        [TestCase(TaxType.Progressive, 2000, "1000")]
        public async Task CalculateTaxAsync_WhenValidPostalCode_ShouldUseCorrectTaxCalculator(TaxType taxType, decimal annualIncome, string postalCode)
        {
            // Arrange
            var loggerMock = Substitute.For<ILogger<TaxCalculatorController>>();
            var factory = TestHelper.GetTaxRateCalculatorFactorySubstitude(annualIncome);
            var taxQueryServiceSubstitude = Substitute.For<ITaxQueryService>();
            taxQueryServiceSubstitude.GetTaxCalculationTypeByPostalCodeAsync(Arg.Any<string>()).Returns(Task.FromResult(taxType));
            var taxCommandServiceSubstitude = Substitute.For<ITaxCommandService>();
            var controller = new TaxCalculatorController(loggerMock, factory, taxQueryServiceSubstitude, taxCommandServiceSubstitude);
            var request = new TaxCalculationRequest
            {
                PostalCode = postalCode,
                AnnualIncome = annualIncome,
            };

            // Act
            var response = await controller.CalculateTaxAsync(request);

            // Assert
            var okResult = response as OkObjectResult;
            factory.Received(1)(Arg.Is<TaxType>(value => value == taxType));
            okResult.ShouldNotBeNull();
            okResult.StatusCode.ShouldBe(200);
            okResult.Value.ShouldBeOfType<ApiResponse<TaxCalculationResponse>>();

            var okResultObj = okResult.Value as ApiResponse<TaxCalculationResponse>;
            okResultObj.Payload.TaxAmountPayable.ShouldBe(annualIncome);
        }
    }
}