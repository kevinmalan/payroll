using NSubstitute;
using NUnit.Framework;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services;
using Payroll.MVC.Services.Contracts;
using Shouldly;
using System;
using System.Threading.Tasks;

namespace Payroll.Tests
{
    public class TestHelper
    {
        public static ITaxRateCalculator GetTaxRateCalculator(TaxType taxType)
        {
            var taxQueryServiceMock = Substitute.For<ITaxQueryService>();
            ITaxRateCalculator flatRateTaxCalculator = new FlatRateTaxCalculator(taxQueryServiceMock);
            ITaxRateCalculator flatValueTaxCalculator = new FlatValueTaxCalculator(taxQueryServiceMock);
            ITaxRateCalculator progressiveTaxCalculator = new ProgressiveTaxCalculator(taxQueryServiceMock);

            return taxType switch
            {
                TaxType.FlatRate => flatRateTaxCalculator,
                TaxType.FlatValue => flatValueTaxCalculator,
                TaxType.Progressive => progressiveTaxCalculator,
                _ => throw new ArgumentException($"Could not find a Tax Rate Calculator for Tax Type '{taxType}'")
            };
        }
    }
}