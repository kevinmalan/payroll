using Payroll.MVC.Services.Contracts;

namespace Payroll.MVC.Services.Factories
{
    public class FlatRateCalculatorFactory : TaxCalculatorFactory
    {
        public override ITaxRateCalculator GetTaxRateCalculator()
        {
            return new FlatRateTaxCalculator();
        }
    }
}