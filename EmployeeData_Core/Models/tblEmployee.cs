using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeData_Core.Models
{
    [Table("tblEmployee")]
    public partial class tblEmployee
    {
        [Key]
        public int Id { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [StringLength(50)]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [StringLength(50)]
        [EmailAddress]
        [Required]
        [Display(Name = "Email")]
        public string EmailId { get; set; }

        [StringLength(1500)]
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [StringLength(500)]
        [Required]
        public string Qualification { get; set; }

        [Required]
        [Display(Name = "Department")]
        public long? DepartmentId { get; set; }

        [NotMapped]
        public string Department { get; set; }

    }
}
