using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Payroll.MVC.Models;
using Payroll.MVC.Models.Enums;

namespace Payroll.MVC.Services
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<PostalCodeCalculationTypeMap>()
                .Property(x => x.CalculationType)
                .HasConversion(new EnumToStringConverter<TaxType>());
        }

        public DbSet<FlatRate> FlatRates { get; set; }
        public DbSet<FlatValue> FlatValues { get; set; }
        public DbSet<ProgressiveRate> ProgressiveRates { get; set; }
        public DbSet<PostalCodeCalculationTypeMap> PostalCodeCalculationTypeMaps { get; set; }
    }
}