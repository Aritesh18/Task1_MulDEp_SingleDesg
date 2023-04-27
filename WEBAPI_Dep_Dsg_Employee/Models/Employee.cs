using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_Dep_Dsg_Employee.Models
{
    /// <summary>
    /// In this table data employee is added 
    /// collections is being used for many to many relation between employee and department 
    /// </summary>
    public class Employee
    {
        public int EmployeeId { get; set; } // primary key
        [Required]
        public string EmployeeName { get; set; }
        [Required]
        public string EmployeeAddress { get; set; }
        [Required]
        public int EmployeeSalary { get; set; }
        public ICollection<DepartmentEmployee> DepartmentEmployees { get; set; } // collection(many to many relationship) for employee and department 
        [Display(Name = "Designation")]
        public int DesignationId { get; set; } // foreign key
        public Designation Designation { get; set; }
    }
}
