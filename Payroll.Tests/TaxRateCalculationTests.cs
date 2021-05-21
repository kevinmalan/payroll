using NUnit.Framework;
using Payroll.MVC.Services;
using Payroll.MVC.Services.Contracts;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace Payroll.Tests
{
    [TestFixture]
    public class TaxRateCalculationTests
    {
        [TestCase(200000, 35000)]
        [TestCase(90540.32, 15844.556)]
        [TestCase(0, 0)]
        public async Task CalculateFlatRateTaxRate_WhenValidRequest_ShouldCalculateCorrectly(decimal annualIncome, decimal expectedTaxRate)
        {
            // Arrange
            ITaxRateCalculator taxCalculator = new FlatRateTaxCalculator();

            // Act
            var taxAmount = await taxCalculator.CalculateTaxAmountAsync(annualIncome);

            // Assert
            taxAmount.ShouldBe(expectedTaxRate);
        }

        [TestCase(-0.1)]
        [TestCase(-1)]
        [TestCase(-999.99)]
        public async Task CalculateFlatRateTaxRate_WhenInvalidRequest_ShouldThrowException(decimal annualIncome)
        {
            // Arrange
            ITaxRateCalculator taxCalculator = new FlatRateTaxCalculator();

            // Act
            var exception = await Should.ThrowAsync<ArgumentException>(async () => await taxCalculator.CalculateTaxAmountAsync(annualIncome));

            // Assert
            exception.Message.ShouldBe($"The provided annual income '{annualIncome}' should not be below 0.");
        }

        [TestCase(300000, 10000)]
        [TestCase(325230.75, 10000)]
        [TestCase(200000, 10000)]
        [TestCase(199999.99, 9999.9995)]
        [TestCase(2500, 125)]
        [TestCase(0, 0)]
        public async Task CalculateFlatValue_WhenValidRequest_ShouldCalculateCorrectly(decimal annualIncome, decimal expectedTaxRate)
        {
            // Arrange
            ITaxRateCalculator taxCalculator = new FlatValueTaxCalculator();

            // Act
            var taxAmount = await taxCalculator.CalculateTaxAmountAsync(annualIncome);

            // Assert
            taxAmount.ShouldBe(expectedTaxRate);
        }

        [TestCase(-0.1)]
        [TestCase(-1)]
        [TestCase(-999.99)]
        public async Task CalculateFlatValueTaxRate_WhenInvalidRequest_ShouldThrowException(decimal annualIncome)
        {
            // Arrange
            ITaxRateCalculator taxCalculator = new FlatValueTaxCalculator();

            // Act
            var exception = await Should.ThrowAsync<ArgumentException>(async () => await taxCalculator.CalculateTaxAmountAsync(annualIncome));

            // Assert
            exception.Message.ShouldBe($"The provided annual income '{annualIncome}' should not be below 0.");
        }

        [TestCase(5000, 500)]
        [TestCase(8350, 835)]
        [TestCase(8360, 836.5)]
        [TestCase(25320.29, 3380.5435)]
        [TestCase(75000, 14102.50)]
        [TestCase(150000, 33616)]
        [TestCase(315292.75, 80857.6075)]
        [TestCase(1000000, 313430)]
        public async Task CalculateProgressiveTaxRate_WhenValidRequest_ShouldCalculateCorrectly(decimal annualIncome, decimal expectedTaxRate)
        {
            // Arrange
            ITaxRateCalculator taxCalculator = new ProgressiveTaxCalculator();

            // Act
            var taxAmount = await taxCalculator.CalculateTaxAmountAsync(annualIncome);

            // Assert
            taxAmount.ShouldBe(expectedTaxRate);
        }
    }
}