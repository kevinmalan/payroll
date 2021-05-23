using Payroll.MVC.Models;
using Payroll.MVC.Models.Enums;
using System.Collections.Generic;

namespace Payroll.MVC.Common
{
    public static class SeedValues
    {
        public static IList<FlatRate> GetFlatRatesSeedValues()
        {
            return new List<FlatRate>
            {
                new FlatRate
                {
                    From = 0,
                    To = decimal.MaxValue,
                    TaxPercentage = 17.5M,
                    AdditionalAmount = 0
                }
            };
        }

        public static IList<FlatValue> GetFlatValueRatesSeedValues()
        {
            return new List<FlatValue>
            {
                new FlatValue
                {
                    From = 0,
                    To = 199999.9999999999M,
                    TaxPercentage = 5M,
                    AdditionalAmount = 0M
                },
                new FlatValue
                {
                    From = 200000M,
                    To = decimal.MaxValue,
                    TaxPercentage = 0M,
                    AdditionalAmount = 10000M
                }
            };
        }

        public static IList<ProgressiveRate> GetProgressiveRateSeedValues()
        {
            return new List<ProgressiveRate>
            {
                new ProgressiveRate
                {
                    RatePercentage = 10M,
                    From = 0M,
                    To = 8350M,
                    AdditionalAmount = 0M
                },
                new ProgressiveRate
                {
                    RatePercentage = 15M,
                    From = 8351M,
                    To = 33950M,
                    AdditionalAmount =  0M
                },
                new ProgressiveRate
                {
                    RatePercentage = 25M,
                    From = 33951M,
                    To = 82250M,
                    AdditionalAmount =  0M
                },
                new ProgressiveRate
                {
                    RatePercentage = 28M,
                    From = 82251M,
                    To = 171550M ,
                    AdditionalAmount =   0M
                },
                new ProgressiveRate
                {
                    RatePercentage = 33M,
                    From = 171551M,
                    To = 372950M,
                    AdditionalAmount =   0M
                },
                new ProgressiveRate
                {
                    RatePercentage = 35M,
                    From = 372951M,
                    To = decimal.MaxValue,
                    AdditionalAmount =   0M
                }
            };
        }

        public static IList<PostalCodeCalculationTypeMap> GetPostalCodeCalculationTypeMap()
        {
            return new List<PostalCodeCalculationTypeMap>
            {
                new PostalCodeCalculationTypeMap
                {
                    PostalCode = "7441",
                    CalculationType = TaxType.Progressive
                },
                new PostalCodeCalculationTypeMap
                {
                    PostalCode = "A100",
                    CalculationType = TaxType.FlatValue
                },
                new PostalCodeCalculationTypeMap
                {
                    PostalCode = "7000",
                    CalculationType = TaxType.FlatRate
                },
                new PostalCodeCalculationTypeMap
                {
                    PostalCode = "1000",
                    CalculationType = TaxType.Progressive
                },
            };
        }
    }
}