using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_Dep_Dsg_Employee.Models
{
    /// <summary>
    /// in this many to many relationship is applied where it contains both foreign keys 
    /// this table is automatically created in database but here we need it in physical manner also 
    /// </summary>
    public class DepartmentEmployee
    {
        public int EmployeeId { get; set; }
        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        public int DepartmentId { get; set; } 
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }
    }
}
