using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_Dep_Dsg_Employee.Models
{
    public class EmployeeVMModel
    {
        
        public int EmployeeId { get; set; } 
        public string EmployeeName { get; set; }
        public string EmployeeAddress { get; set; }
        public int EmployeeSalary { get; set; }
        public int DesignationId { get;  set; }
        public string DesignationName { get; set; }
        public List<int> DepartmentId { get; set; }
        public List<string> DepartmentName { get;  set; }

    }
}
