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
        [Test]
        public async Task CalculateFlatRateTaxRate_WhenValidRequest_ShouldCalculateCorrectly()
        {
            // Arrange
            ITaxRateCalculator taxCalculator = new FlatRateCalculator();

            // Act
            var taxAmount1 = await taxCalculator.CalculateTaxAmountAsync(200000);
            var taxAmount2 = await taxCalculator.CalculateTaxAmountAsync(90540.32M);
            var taxAmount3 = await taxCalculator.CalculateTaxAmountAsync(0);

            // Assert
            taxAmount1.ShouldBe(35000M);
            taxAmount2.ShouldBe(15844.556M);
            taxAmount3.ShouldBe(0);
        }

        [Test]
        public async Task CalculateFlatRateTaxRate_WhenInvalidRequest_ShouldThrowException()
        {
            // Arrange
            ITaxRateCalculator taxCalculator = new FlatRateCalculator();

            // Act
            var exception = await Should.ThrowAsync<ArgumentException>(async () => await taxCalculator.CalculateTaxAmountAsync(-200000));

            // Assert
            exception.GetType().ShouldBe(typeof(ArgumentException));
            exception.Message.ShouldBe("The provided annual income '-200000' should not be below 0.");
        }
    }
}