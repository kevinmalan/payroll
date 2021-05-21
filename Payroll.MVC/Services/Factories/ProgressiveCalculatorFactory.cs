using Payroll.MVC.Services.Contracts;

namespace Payroll.MVC.Services.Factories
{
    public class ProgressiveCalculatorFactory : TaxCalculatorFactory
    {
        public override ITaxRateCalculator GetTaxRateCalculator()
        {
            return new ProgressiveTaxCalculator();
        }
    }
}