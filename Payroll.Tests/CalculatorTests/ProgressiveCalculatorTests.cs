using NUnit.Framework;
using Payroll.MVC.Common;
using Payroll.MVC.Services;
using Shouldly;
using System.Threading.Tasks;

namespace Payroll.Tests.CalculatorTests
{
    [TestFixture]
    public class ProgressiveCalculatorTests : BaseTest
    {
        [TestCase(5000, 500)]
        [TestCase(8350, 835)]
        [TestCase(8350.50, 835.075)]
        [TestCase(8360, 836.5)]
        [TestCase(25320.29, 3380.5435)]
        [TestCase(75000, 14102.50)]
        [TestCase(150000, 33616)]
        [TestCase(315292.75, 80857.6075)]
        [TestCase(1000000, 313430)]
        public async Task CalculateProgressiveTaxRate_WhenValidRequest_ShouldCalculateCorrectly(decimal annualIncome, decimal expectedTaxRate)
        {
            // Arrange
            var db = Db();
            db.ProgressiveRates.AddRange(SeedValues.GetProgressiveRateSeedValues());
            db.SaveChanges();
            var taxCalculator = new ProgressiveTaxCalculator(new TaxQueryService(db));

            // Act
            var taxAmount = await taxCalculator.CalculateTaxAmountAsync(annualIncome);

            // Assert
            taxAmount.ShouldBe(expectedTaxRate);
        }
    }
}