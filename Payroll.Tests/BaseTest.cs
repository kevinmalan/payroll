using Microsoft.EntityFrameworkCore;
using Payroll.MVC.Services;
using System;

namespace Payroll.Tests
{
    public class BaseTest
    {
        protected static DataContext Db()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

            return new DataContext(options);
        }
    }
}