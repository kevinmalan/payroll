using NUnit.Framework;
using Payroll.MVC.Common;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services;
using Shouldly;
using System.Threading.Tasks;

namespace Payroll.Tests.CalculatorTests
{
    [TestFixture]
    public class TaxCalculatorTypeTests : BaseTest
    {
        [TestCase("7441", TaxType.Progressive)]
        [TestCase("A100", TaxType.FlatValue)]
        [TestCase("7000", TaxType.FlatRate)]
        [TestCase("1000", TaxType.Progressive)]
        public async Task GetTaxCalculationType_WhenValidPostalCode_ShouldReturnCorrectCalculationType(string postalCode, TaxType expectedTaxType)
        {
            // Arrange
            var db = Db();
            db.PostalCodeCalculationTypeMaps.AddRange(SeedValues.GetPostalCodeCalculationTypeMap());
            db.SaveChanges();
            var queryService = new TaxQueryService(db);

            // Act
            var taxCalcType = await queryService.GetTaxCalculationTypeByPostalCodeAsync(postalCode);

            // Assert
            taxCalcType.ShouldBe(expectedTaxType);
        }
    }
}