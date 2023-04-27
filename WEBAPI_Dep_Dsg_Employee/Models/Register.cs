using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_Dep_Dsg_Employee.Models
{
  public class Register : ApplicationUser
  {
    public string EmployeeName { get; set; }
    public string EmployeeAddress { get; set; }
    public string EmployeeSalary { get; set; }
    public string RegisterEmail { get; set; }
    public string RegisterPassword { get; set; }
  }
}
