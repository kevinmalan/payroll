using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Payroll.MVC.Models.Enums;
using Payroll.MVC.Services;
using Payroll.MVC.Services.Contracts;
using System;
using System.IO;
using System.Text.Json.Serialization;

namespace Payroll.MVC
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews()
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            services.AddDbContext<DataContext>(options => options.UseSqlServer(Configuration["TaxCalcDb"].Replace("[CurrentDirectory]", Directory.GetCurrentDirectory())));

            services.AddScoped<ITaxQueryService, TaxQueryService>();
            services.AddScoped<FlatRateTaxCalculator>();
            services.AddScoped<FlatValueTaxCalculator>();
            services.AddScoped<ProgressiveTaxCalculator>();

            services.AddScoped<Func<TaxType, ITaxRateCalculator>>(serviceProvider => taxType => taxType switch
            {
                TaxType.FlatRate => serviceProvider.GetRequiredService<FlatRateTaxCalculator>(),
                TaxType.FlatValue => serviceProvider.GetRequiredService<FlatValueTaxCalculator>(),
                TaxType.Progressive => serviceProvider.GetRequiredService<ProgressiveTaxCalculator>(),
                _ => throw new ArgumentException($"Could not find a Tax Rate Calculator for Tax Type '{taxType}'")
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}