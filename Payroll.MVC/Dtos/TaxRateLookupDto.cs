namespace Payroll.MVC.Dtos
{
    public class TaxRateLookupDto
    {
        public decimal RateFrom { get; set; }
        public decimal RateTo { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal AdditionalAmount { get; set; }
    }
}