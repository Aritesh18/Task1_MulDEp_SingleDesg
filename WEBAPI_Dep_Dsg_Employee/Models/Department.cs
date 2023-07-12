using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_Dep_Dsg_Employee.Models
{
    /// <summary>
    /// In this model primary key created for department and having a collection of department and employee where many to many realtionship is being applied
    /// </summary>
    public class Department
    {
        public int DepartmentId { get; set; }
        public string DepartmentName { get; set; }
        public ICollection<DepartmentEmployee> DepartmentEmployees { get; set; }// collection of department and employee 
    }
}
