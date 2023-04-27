using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_Dep_Dsg_Employee.Data;

namespace WEBAPI_Dep_Dsg_Employee.Identity
{
  public class ApplicationRoleStore:RoleStore<ApplicationRole,ApplicationDbContext>
  {
    public ApplicationRoleStore(ApplicationDbContext context, IdentityErrorDescriber errorDescriber) : base(context, errorDescriber)
    {

    }
  }
}
