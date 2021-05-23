using NSubstitute;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Payroll.Tests
{
    public class TestHelper
    {
        public static ITaxRateCalculator GetTaxRateCalculatorMock(decimal amount)
        {
            var calculatorMock = Substitute.For<ITaxRateCalculator>();
            calculatorMock.CalculateTaxAmountAsync(Arg.Any<decimal>()).Returns(Task.FromResult(amount));

            return calculatorMock;
        }

        public static Func<TaxType, ITaxRateCalculator> GetTaxRateCalculatorFactory()

            => new(taxType => taxType switch
            {
                TaxType.FlatRate => GetTaxRateCalculatorMock(100M),
                TaxType.FlatValue => GetTaxRateCalculatorMock(120M),
                TaxType.Progressive => GetTaxRateCalculatorMock(130M),
                _ => throw new ArgumentException($"Could not find a Tax Rate Calculator for Tax Type '{taxType}'")
            });
    }
}