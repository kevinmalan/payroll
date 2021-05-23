using NUnit.Framework;
using Payroll.MVC.Common;
using Payroll.MVC.Services;
using Shouldly;
using System.Threading.Tasks;

namespace Payroll.Tests.CalculatorTests
{
    [TestFixture]
    public class FlatRateCalculatorTests : BaseTest
    {
        [TestCase(200000, 35000)]
        [TestCase(90540.32, 15844.556)]
        [TestCase(0, 0)]
        public async Task CalculateFlatRateTaxRate_WhenValidRequest_ShouldCalculateCorrectly(decimal annualIncome, decimal expectedTaxRate)
        {
            // Arrange
            var db = Db();
            db.FlatRate.AddRange(SeedValues.GetFlatRatesSeedValues());
            db.SaveChanges();
            var taxCalculator = new FlatRateTaxCalculator(new TaxQueryService(db));

            // Act
            var taxAmount = await taxCalculator.CalculateTaxAmountAsync(annualIncome);

            // Assert
            taxAmount.ShouldBe(expectedTaxRate);
        }
    }
}