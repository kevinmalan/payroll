using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shouldly;
using Payroll.MVC.Services.Contracts;
using Payroll.MVC.Services;

namespace Payroll.Tests
{
    [TestFixture]
    public class TaxRateCalculationTests
    {
        [Test]
        public async Task CalculateTaxRate_WhenFlatRate_ShouldCalculateCorrectly()
        {
            // Arrange
            ITaxRateCalculator taxCalculator = new TaxRateCalculator();

            // Act
            var taxAmount = await taxCalculator.CalculateTaxAmountAsync(200000);

            // Assert
            taxAmount.ShouldBe(35000M);
        }
    }
}