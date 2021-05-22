using System;

namespace Payroll.MVC.Services
{
    public class TaxHelperService
    {
        public static void ValidateAnnualIncome(decimal annualIncome)
        {
            if (annualIncome < 0M)
            {
                throw new ArgumentException($"The provided annual income '{annualIncome}' should not be below 0.");
            }
        }

        public static decimal CalculateAmountPercentage(decimal amount, decimal percentage)
        {
            return amount * (percentage / 100M);
        }
    }
}