using Payroll.MVC.Services.Contracts;

namespace Payroll.MVC.Services.Factories
{
    public class FlatValueCalculatorFactory : TaxCalculatorFactory
    {
        public override ITaxRateCalculator GetTaxRateCalculator()
        {
            return new FlatValueTaxCalculator();
        }
    }
}