using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeData_Core.Models
{
    [Table("tblDepartment")]
    public partial class tblDepartment
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name ="Department Name")]
        public string DepartmentName { get; set; }
    }
}
