using Microsoft.EntityFrameworkCore;
using Payroll.MVC.Dtos.Requests;
using Payroll.MVC.Dtos.Responses;
using Payroll.MVC.Models;
using Payroll.MVC.Services.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Payroll.MVC.Services
{
    public class TaxCommandService : ITaxCommandService
    {
        private readonly DataContext _dataContext;

        public TaxCommandService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateTaxCalculationHistoryAsync(TaxCalculatorHistoryRequest request)
        {
            await _dataContext.TaxCalculationHistory.AddAsync(
                new TaxCalculationHistory
                {
                    AnnualIncome = request.AnnualIncome,
                    PostalCode = request.PostalCode,
                    CalculatedTax = request.CalculatedTax,
                    CalculationType = request.CalculationType
                });

            await _dataContext.SaveChangesAsync();
        }
    }
}