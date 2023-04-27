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
  [Route("api/department")]
    [ApiController]
        public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public DepartmentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult GetDepartment()
        {
            var departmentInDb = from Department in _context.Departments 
                                 select new Department      
                                 {
                                     DepartmentId = Department.DepartmentId,
                                     DepartmentName = Department.DepartmentName
                                 };
            return Ok(departmentInDb);
        }
        [HttpGet("{id}")]
        public IActionResult GetDepartmentByEmployeeId(int id) 
        {
            var departmentFromDb = _context.DepartmentEmployees.Include(e => e.Department).Where(ep => ep.EmployeeId == id).ToList();
            if (departmentFromDb == null) return NotFound();
            return Ok(departmentFromDb);
        }
        [HttpPost]
        public IActionResult SaveDepartment([FromBody] Department department)
        {
            if (department != null && ModelState.IsValid)
            {
                _context.Departments.Add(department); 
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpPut]
        public IActionResult UpdateDepartment([FromBody] Department department)
        {
            if (department != null && ModelState.IsValid)
            {
                _context.Departments.Update(department);
                _context.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        [HttpDelete("{id:int}")]
        public IActionResult DeleteDepartment(int id)
        {
            var depfromdb = _context.Departments.Find(id);
            if (depfromdb == null) return NotFound();
            _context.Departments.Remove(depfromdb);
            _context.SaveChanges();
            return Ok();
        }
    }
}
