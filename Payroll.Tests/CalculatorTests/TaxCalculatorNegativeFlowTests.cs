using NUnit.Framework;
using Payroll.MVC.Services;
using Payroll.MVC.Services.Contracts;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace Payroll.Tests.CalculatorTests
{
    [TestFixture]
    public class TaxCalculatorNegativeFlowTests : BaseTest
    {
        [TestCase(-0.1)]
        [TestCase(-1)]
        [TestCase(-999.99)]
        public async Task CalculateTaxRate_WhenInvalidRequest_ShouldThrowException(decimal annualIncome)
        {
            await CalculateTaxRate_WhenInvalidRequest_ShouldThrowException<FlatValueTaxCalculator>(annualIncome);
            await CalculateTaxRate_WhenInvalidRequest_ShouldThrowException<FlatRateTaxCalculator>(annualIncome);
            await CalculateTaxRate_WhenInvalidRequest_ShouldThrowException<ProgressiveTaxCalculator>(annualIncome);
        }

        private async Task CalculateTaxRate_WhenInvalidRequest_ShouldThrowException<T>(decimal annualIncome) where T : ITaxRateCalculator, new()
        {
            // Arrange
            var taxCalculator = new T();

            // Act
            var exception = await Should.ThrowAsync<ArgumentException>(async () => await taxCalculator.CalculateTaxAmountAsync(annualIncome));

            // Assert
            exception.Message.ShouldBe($"The provided annual income '{annualIncome}' should not be below 0.");
        }
    }
}