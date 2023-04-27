using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_Dep_Dsg_Employee.ViewModels;

namespace WEBAPI_Dep_Dsg_Employee.ServiceContract
{
  public interface IUserService
  {
    Task<ApplicationUser> Authenticate(LoginViewModel loginViewModel);
  }
}
