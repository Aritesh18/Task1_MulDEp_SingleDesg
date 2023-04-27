using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_Dep_Dsg_Employee.Data;
using WEBAPI_Dep_Dsg_Employee.Models;

namespace WEBAPI_Dep_Dsg_Employee.Controllers
{

  [Authorize(Roles = SD.Role_Admin)]
  [Route("api/designation")]
    [ApiController]
    public class DesignationController : Controller
    {
        private readonly ApplicationDbContext _context;
        public DesignationController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetDesignation()
        {
            return Ok(_context.Designations.ToList());
        }
        [HttpGet("{id}")]
        public IActionResult GetDesignationByEmployeeId(int id)
        {
            var designationOfEmployee = _context.Employees.Include(x => x.Designation).Where(x => x.EmployeeId == id).ToList();
            if (designationOfEmployee == null) return NotFound();
            return Ok(designationOfEmployee);
        }

        [HttpPost]
        public IActionResult SaveDesignation([FromBody] Designation designation)
        {
            if (designation != null && ModelState.IsValid)
            {
                _context.Designations.Add(designation);
                _context.SaveChanges();
                return Ok();

            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateDesignation([FromBody] Designation designation)
        {
            if (designation != null && ModelState.IsValid)
            {
                _context.Designations.Update(designation);
                _context.SaveChanges();
                return Ok();

            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteDesignation(int id)
        {
            var desFromDb = _context.Designations.Find(id);
            if (desFromDb == null) return NotFound();
            _context.Designations.Remove(desFromDb);
            _context.SaveChanges();
            return Ok();

        }
    }
}
