using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using WEBAPI_Dep_Dsg_Employee;
using WEBAPI_Dep_Dsg_Employee.Data;
using WEBAPI_Dep_Dsg_Employee.Models;

namespace WEBApi_Dep_Dep_Employee.Controllers
{
  [Route("api/user")]
  [ApiController]
  public class UserController : Controller
  {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;

    public UserController(ApplicationDbContext context, UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager)
    {
      _context = context;
      _userManager = userManager;
      _signInManager = signInManager;
    }

    // Register New User having UserName and UserEmail as same column
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] Register register)
    {
      var user = new ApplicationUser { UserName = register.EmployeeName, Email = register.RegisterEmail };
      var result = await _userManager.CreateAsync(user, register.RegisterPassword);
      if (result.Succeeded)
      {
        // Assign the role to the user
        await _userManager.AddToRoleAsync(user, SD.Role_Employee);

        // Return the response
        return Ok();
      }
      return BadRequest(result.Errors);
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] Login login)
    {
      var result = await _signInManager.PasswordSignInAsync(login.LoginEmail, login.LoginPassword, isPersistent: false, lockoutOnFailure: false);
      if (result.Succeeded) return Ok(); //// User was successfully logged in
      return BadRequest("Invalid login attempt.");
    }
  }
}
