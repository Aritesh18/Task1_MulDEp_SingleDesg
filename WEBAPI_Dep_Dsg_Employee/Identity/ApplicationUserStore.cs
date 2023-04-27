using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_Dep_Dsg_Employee.Data;

namespace WEBAPI_Dep_Dsg_Employee.Identity
{
  public class ApplicationUserStore : UserStore<ApplicationUser>
  {
    public ApplicationUserStore(ApplicationDbContext context) : base(context)
    {

    }
  }
}
