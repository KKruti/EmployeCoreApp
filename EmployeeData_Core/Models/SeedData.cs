using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeData_Core.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new EmployeeContext(
                serviceProvider.GetRequiredService<DbContextOptions<EmployeeContext>>()))
            {
                if (context.tblEmployee.Any())
                {
                    return;
                }
            }
        }
    }

}
