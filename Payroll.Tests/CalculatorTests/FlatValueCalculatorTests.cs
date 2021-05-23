using NUnit.Framework;
using Payroll.MVC.Common;
using Payroll.MVC.Services;
using Shouldly;
using System.Threading.Tasks;

namespace Payroll.Tests.CalculatorTests
{
    [TestFixture]
    public class FlatValueCalculatorTests : BaseTest
    {
        [TestCase(300000, 10000)]
        [TestCase(325230.75, 10000)]
        [TestCase(200000, 10000)]
        [TestCase(199999.99, 9999.9995)]
        [TestCase(2500, 125)]
        [TestCase(0, 0)]
        public async Task CalculateFlatValue_WhenValidRequest_ShouldCalculateCorrectly(decimal annualIncome, decimal expectedTaxRate)
        {
            // Arrange
            var db = Db();
            db.FlatValue.AddRange(SeedValues.GetFlatValueRatesSeedValues());
            db.SaveChanges();
            var taxCalculator = new FlatValueTaxCalculator(new TaxQueryService(db));

            // Act
            var taxAmount = await taxCalculator.CalculateTaxAmountAsync(annualIncome);

            // Assert
            taxAmount.ShouldBe(expectedTaxRate);
        }
    }
}