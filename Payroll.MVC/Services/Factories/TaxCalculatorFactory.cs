using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services.Contracts;
using System;

namespace Payroll.MVC.Services.Factories
{
    public abstract class TaxCalculatorFactory
    {
        public abstract ITaxRateCalculator GetTaxRateCalculator();

        public static TaxCalculatorFactory GetFactory(TaxType taxType)
        {
            if (taxType == TaxType.FlatRate)
            {
                return new FlatRateCalculatorFactory();
            }
            else if (taxType == TaxType.FlatValue)
            {
                return new FlatValueCalculatorFactory();
            }
            else if (taxType == TaxType.Progressive)
            {
                return new ProgressiveCalculatorFactory();
            }

            throw new Exception();
        }
    }
}