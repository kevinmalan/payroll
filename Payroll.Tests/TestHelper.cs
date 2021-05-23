using NSubstitute;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services.Contracts;
using System;
using System.Threading.Tasks;

namespace Payroll.Tests
{
    public class TestHelper
    {
        public static Func<TaxType, ITaxRateCalculator> GetTaxRateCalculatorFactorySubstitude(decimal amount)
        {
            var factorySubstitude = Substitute.For<Func<TaxType, ITaxRateCalculator>>();
            var calculatorSubstitude = Substitute.For<ITaxRateCalculator>();

            calculatorSubstitude.CalculateTaxAmountAsync(Arg.Any<decimal>()).Returns(Task.FromResult(amount));
            factorySubstitude(Arg.Any<TaxType>()).Returns(calculatorSubstitude);

            return factorySubstitude;
        }
    }
}