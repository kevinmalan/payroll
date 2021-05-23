using Payroll.MVC.Common;
using Payroll.MVC.Services;
using System.Linq;

namespace Payroll.MVC.Extensions
{
    public static class DataContextExtensions
    {
        private static DataContext _dataContext;

        public static void Seed(this DataContext dataContext)
        {
            _dataContext = dataContext;

            if (!_dataContext.PostalCodeCalculationTypeMap.Any())
            {
                _dataContext.PostalCodeCalculationTypeMap.AddRange(SeedValues.GetPostalCodeCalculationTypeMap());
            }

            if (!_dataContext.FlatRate.Any())
            {
                _dataContext.FlatRate.AddRange(SeedValues.GetFlatRatesSeedValues());
            }

            if (!_dataContext.FlatValue.Any())
            {
                _dataContext.FlatValue.AddRange(SeedValues.GetFlatValueRatesSeedValues());
            }

            if (!_dataContext.ProgressiveRate.Any())
            {
                _dataContext.ProgressiveRate.AddRange(SeedValues.GetProgressiveRateSeedValues());
            }

            _dataContext.SaveChanges();
        }
    }
}