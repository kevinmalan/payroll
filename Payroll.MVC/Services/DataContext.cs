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

        public DbSet<FlatRate> FlatRate { get; set; }
        public DbSet<FlatValue> FlatValue { get; set; }
        public DbSet<ProgressiveRate> ProgressiveRate { get; set; }
        public DbSet<PostalCodeCalculationTypeMap> PostalCodeCalculationTypeMap { get; set; }
    }
}