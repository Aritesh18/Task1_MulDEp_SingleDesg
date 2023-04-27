using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_Dep_Dsg_Employee.Models
{
    /// <summary>
    /// In this model primary key created for designation
    /// </summary>
    public class Designation
    {
        public int DesignationId { get; set; }
        public string DesignationName { get; set; }
        //public ICollection<Employee>Employees { get; set; }
    }
}
