using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EmployeeData_Core.Models;

namespace EmployeeData_Core.Models
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options)
            : base(options)
        {
        }

        public DbSet<EmployeeData_Core.Models.tblEmployee> tblEmployee { get; set; }

        public DbSet<EmployeeData_Core.Models.tblDepartment> tblDepartment { get; set; }
    }
}
