using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using NUnit.Framework;
using Payroll.MVC.Common;
using Payroll.MVC.Controllers;
using Payroll.MVC.Dtos.Requests;
using Payroll.MVC.Dtos.Responses;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services;
using Payroll.MVC.Services.Contracts;
using Shouldly;
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
        public async Task Get_WhenRequestIsValidTaxType_ShouldReturnCorrectTaxCalculator(TaxType taxType, decimal annualIncome, string postalCode)
        {
            // Arrange
            var loggerMock = Substitute.For<ILogger<TaxCalculatorController>>();
            var factory = TestHelper.GetTaxRateCalculatorFactorySubstitude(annualIncome);

            var db = Db();
            db.PostalCodeCalculationTypeMaps.AddRange(SeedValues.GetPostalCodeCalculationTypeMap());
            db.SaveChanges();

            var taxQueryServiceSubstitude = new TaxQueryService(db);

            var controller = new TaxCalculatorController(loggerMock, factory, taxQueryServiceSubstitude);
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
            okResult.Value.ShouldBeOfType<TaxCalculationResponse>();

            var okResultObj = okResult.Value as TaxCalculationResponse;
            okResultObj.TaxAmountPayable.ShouldBe(annualIncome);
        }
    }
}