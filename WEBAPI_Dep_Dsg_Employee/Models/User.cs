using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WEBAPI_Dep_Dsg_Employee.Models
{
    /// <summary>
    /// In this model user properties are applied where token is not mapped because we do not need it in the database
    /// </summary>
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
        [NotMapped]
        public string Token { get; set; }
    }
}
