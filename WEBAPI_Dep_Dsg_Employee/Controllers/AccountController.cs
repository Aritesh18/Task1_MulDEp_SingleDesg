using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_Dep_Dsg_Employee.ServiceContract;
using WEBAPI_Dep_Dsg_Employee.ViewModels;

namespace WEBAPI_Dep_Dsg_Employee.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class AccountController : Controller
  {
    private readonly IUserService _userService;
    public AccountController(IUserService userService)
    {
      _userService = userService;
    }
    [HttpPost]
    [Route("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] LoginViewModel loginViewModel)
    {
      var user = await _userService.Authenticate(loginViewModel);
      if (user == null) return BadRequest(new { message = "Wrong User/Password" });
      return Ok(user);
    }
  }
}
