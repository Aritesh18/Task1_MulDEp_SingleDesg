using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBAPI_Dep_Dsg_Employee.Data;
using WEBAPI_Dep_Dsg_Employee.Models;

namespace WEBAPI_Dep_Dsg_Employee.Controllers
{

/*  [Authorize(Roles = SD.Role_Admin + "," + SD.Role_Employee)]
*/  [Route("api/employee")]
    [ApiController]
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
    private readonly ILogger<EmployeeController> _logger;
    public EmployeeController(ApplicationDbContext context, ILogger<EmployeeController> logger)
    {
      _context = context;
      _logger = logger;
    }
    [HttpGet]
        public IActionResult GetEmployee()
        {
      _logger.LogInformation("GetEmployee() Called");
           
            var employeeFromDb = (from employee in _context.Employees
                                  join EmployeeDepartment in _context.DepartmentEmployees  
                                  on employee.EmployeeId equals EmployeeDepartment.EmployeeId 
                                  select new EmployeeVMModel
                                  {
                                      // Map the database results to an EmployeeVMModel ViewModel.
                                      EmployeeId = employee.EmployeeId,
                                      EmployeeName = employee.EmployeeName,
                                      EmployeeAddress = employee.EmployeeAddress,
                                      EmployeeSalary = employee.EmployeeSalary,
                                      DesignationName = employee.Designation.DesignationName,
                                      DesignationId = employee.DesignationId,
                                      DepartmentName = _context.DepartmentEmployees
                                          .Where(departmentEmployee => departmentEmployee.EmployeeId == employee.EmployeeId)
                                          .Select(employee => employee.Department.DepartmentName).ToList(),
                                      DepartmentId = _context.DepartmentEmployees
                                          .Where(departmentEmployee => departmentEmployee.EmployeeId == employee.EmployeeId)
                                          .Select(employee => employee.Department.DepartmentId).ToList(),
                                  });
     _logger.LogInformation("lINQ Executed");

      // Create an empty list of EmployeeVMModel objects.
      List<EmployeeVMModel> employeeVMModels = new List<EmployeeVMModel>();

            // Iterate over the mapped database results and check for duplicate employee IDs.

            foreach (var employee in employeeFromDb)
            {
        _logger.LogInformation(" in Foreach");

        if (employeeVMModels.FirstOrDefault(employeeList => employeeList.EmployeeId == employee.EmployeeId) == null)
                {
                    // Add the EmployeeVMModel object to the employeeVMModels list if it does not exist.
                    employeeVMModels.Add(employee);
                }
            }

         
            return Ok(employeeVMModels);
        }


        [HttpGet("{id:int}")]
        public IActionResult GetEmployeeById(int id)
        {
            var employeeInDb = _context.Employees.Include(employee => employee.Designation).FirstOrDefault(employee => employee.EmployeeId == id);
            if (employeeInDb == null)
            {
                return NotFound();
            }
            var departmentInDb = _context.DepartmentEmployees
                .Where(departmentEmployee => departmentEmployee.EmployeeId == employeeInDb.EmployeeId)
                .Join(_context.Departments, departmentEmployee => departmentEmployee.DepartmentId, department => department.DepartmentId,
                (departmentEmployee, department) => new { department.DepartmentName });

            var departmentNamesList = departmentInDb.Select(d => d.DepartmentName).ToList();

            var employeeVMModels = new EmployeeVMModel
            {
                EmployeeId = employeeInDb.EmployeeId,
                EmployeeName = employeeInDb.EmployeeName,
                EmployeeAddress = employeeInDb.EmployeeAddress,
                EmployeeSalary = employeeInDb.EmployeeSalary,
                DepartmentName = departmentNamesList,
                DesignationId = employeeInDb.DesignationId,
                DesignationName = employeeInDb.Designation.DesignationName,
            };
            return Ok(employeeVMModels);
        }

        [HttpPost]
        public IActionResult SaveEmployee([FromBody] EmployeeVMModel employeeVMModel)
        {
      _logger.LogInformation("employee detail");


      if (ModelState.IsValid && employeeVMModel != null)
            {
                var employee = new Employee
                {
                    EmployeeName = employeeVMModel.EmployeeName,
                    EmployeeAddress = employeeVMModel.EmployeeAddress,
                   EmployeeSalary = employeeVMModel.EmployeeSalary, 
                    DesignationId = employeeVMModel.DesignationId
                };

                _context.Employees.Add(employee);
                _context.SaveChanges();    

                var departmentEmployees = employeeVMModel.DepartmentId
                    .Select(departmentId => new DepartmentEmployee
                    {
                        EmployeeId = employee.EmployeeId,
                        DepartmentId = departmentId
                    })
                    .ToList();

                _context.DepartmentEmployees.AddRange(departmentEmployees);
                _context.SaveChanges();
                return Ok();

      }
      else
            {
                return BadRequest();
            }

        }
    //[HttpPut]
    //public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeVMModel employeeVMModel)
    //{
    //  if (ModelState.IsValid && employeeVMModel != null)
    //  {
    //    // Updating and Saving Employees and Designation
    //    //var employee = await _context.Employees.FindAsync(employeeVMModel.EmployeeId);
    //    //employee.DesignationId = employeeVMModel.DesignationId;
    //    var employee = new Employee
    //    {

    //      EmployeeName = employeeVMModel.EmployeeName,
    //      EmployeeAddress = employeeVMModel.EmployeeAddress,
    //      EmployeeSalary = employeeVMModel.EmployeeSalary,
    //      DesignationId = employeeVMModel.DesignationId,
    //    };
    //    await _context.Employees.AddAsync(employee);
    //    await _context.SaveChangesAsync();

    //    // Updating the assigned Departments to the Employees

    //    //List<DepartmentEmployee> departmentEmployees = new List<DepartmentEmployee>();
    //    //foreach (var empDep in employeeVMModel.DepartmentId)
    //    //{
    //    //    if (!_context.DepartmentEmployees.Any(de => de.EmployeeId == employee.EmployeeId && de.DepartmentId == empDep))
    //    //    {
    //    //        DepartmentEmployee departmentEmployee = new DepartmentEmployee()
    //    //        {

    //    //            EmployeeId = employee.EmployeeId,
    //    //            DepartmentId = empDep
    //    //        };
    //    //        departmentEmployees.Add(departmentEmployee);
    //    //    }
    //    //}
    //    //_context.DepartmentEmployees.AddRange(departmentEmployees);
    //    //_context.RemoveRange(_context.DepartmentEmployees.Where(departmentEmployee => departmentEmployee.EmployeeId == employee.EmployeeId && !employeeVMModel.DepartmentId.Contains(departmentEmployee.DepartmentId)));
    //    var deptemp = employeeVMModel.DepartmentId
    //     .Select(deptId => new DepartmentEmployee
    //     {
    //       EmployeeId = employee.EmployeeId,
    //       DepartmentId = deptId
    //     })
    //     .ToList();

    //    await _context.DepartmentEmployees.AddRangeAsync(deptemp);
    //    await _context.SaveChangesAsync();
    //    return Ok();
    //  }
    //  return BadRequest();
    //}
    [HttpPut]
        public async Task<IActionResult> UpdateEmployee([FromBody] EmployeeVMModel employeeVMModel)
        {
            if (employeeVMModel != null && ModelState.IsValid)
            {
        // Updating and Saving Employees and Designation

        //var employee = await _context.Employees.FindAsync(employeeVMModel.EmployeeId);
        //employee.DesignationId = employeeVMModel.DesignationId;
        Employee employee = new Employee()
                {
                  EmployeeId= employeeVMModel.EmployeeId,
                    EmployeeName = employeeVMModel.EmployeeName,
                    EmployeeAddress = employeeVMModel.EmployeeAddress,
                    EmployeeSalary = employeeVMModel.EmployeeSalary, 
                   DesignationId = employeeVMModel.DesignationId,
                };
                _context.Employees.Update(employee);
                _context.SaveChanges();

        // Updating the assigned Departments to the Employees

        List<DepartmentEmployee> departmentEmployees = new List<DepartmentEmployee>();
                foreach (var depemp in employeeVMModel.DepartmentId)
                {
                    if (!_context.DepartmentEmployees.Any(de => de.EmployeeId == employee.EmployeeId && de.DepartmentId == depemp))
                    {
                        DepartmentEmployee departmentEmployee = new DepartmentEmployee()
                        {
                            EmployeeId = employee.EmployeeId,
                            DepartmentId = depemp
                        };
                        departmentEmployees.Add(departmentEmployee);
                    }
                }
                _context.DepartmentEmployees.AddRange(departmentEmployees);
                _context.RemoveRange(_context.DepartmentEmployees.Where(ed => ed.EmployeeId == employee.EmployeeId && !employeeVMModel.DepartmentId.Contains(ed.DepartmentId)));

                await _context.SaveChangesAsync();
                return Ok();
            }
            return BadRequest();
        }
    //[HttpPut]
    //public async Task<IActionResult> PutEmployee(EmployeeVMModel employeeVMModel)
    //{
    //  var employee = await _context.Employees.Include(e => e.DepartmentEmployees).FirstOrDefaultAsync(e => e.EmployeeId == id);

    //  if (employee == null)
    //  {
    //    return NotFound();
    //  }

    //  employee.EmployeeName = employeeVMModel.EmployeeName;
    //  employee.EmployeeAddress = employeeVMModel.EmployeeAddress;
    //  employee.EmployeeSalary = employeeVMModel.EmployeeSalary;
    //  employee.DesignationId = employeeVMModel.DesignationId;

    //   Remove all current department employees for this employee
    //  employee.DepartmentEmployees.Clear();

    //   Add new department employees for this employee
    //  foreach (var departmentId in employeeVMModel.DepartmentId)
    //  {
    //    employee.DepartmentEmployees.Add(new DepartmentEmployee
    //    {
    //      DepartmentId = departmentId
    //    });
    //  }

    //  try
    //  {
    //    await _context.SaveChangesAsync();
    //  }
    //  catch (DbUpdateConcurrencyException)
    //  {
    //    if (!EmployeeExists(id))
    //    {
    //      return NotFound();
    //    }
    //    else
    //    {
    //      throw;
    //    }
    //  }

    //  return NoContent();
    //}

    //private bool EmployeeExists(int id)
    //{
    //  return _context.Employees.Any(e => e.EmployeeId == id);
    //}

    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateEmployee(int id, EmployeeVMModel employeeVM)
    //{
    //  if (id != employeeVM.EmployeeId)
    //  {
    //    return BadRequest();
    //  }

    //  var employee = await _context.Employees
    //      .Include(e => e.DepartmentEmployees)
    //      .FirstOrDefaultAsync(e => e.EmployeeId == id);

    //  if (employee == null)
    //  {
    //    return NotFound();
    //  }

    //  employee.EmployeeName = employeeVM.EmployeeName;
    //  employee.EmployeeAddress = employeeVM.EmployeeAddress;
    //  employee.EmployeeSalary = employeeVM.EmployeeSalary;
    //  employee.DesignationId = employeeVM.DesignationId;

    //  // Remove existing department employees that are not included in the new list
    //  var existingDepartmentIds = employee.DepartmentEmployees.Select(de => de.DepartmentId).ToList();
    //  var newDepartmentIds = employeeVM.DepartmentId;
    //  var removedDepartmentIds = existingDepartmentIds.Except(newDepartmentIds).ToList();
    //  var removedDepartmentEmployees = employee.DepartmentEmployees.Where(de => removedDepartmentIds.Contains(de.DepartmentId)).ToList();
    //  _context.DepartmentEmployees.RemoveRange(removedDepartmentEmployees);

    //  // Add new department employees that are not already in the existing list
    //  var newDepartmentEmployees = newDepartmentIds
    //      .Where(de => !existingDepartmentIds.Contains(de))
    //      .Select(de => new DepartmentEmployee { DepartmentId = de, EmployeeId = employee.EmployeeId })
    //      .ToList();
    //  employee.DepartmentEmployees.Add(newDepartmentEmployees);

    //  try
    //  {
    //    await _context.SaveChangesAsync();
    //  }
    //  catch (DbUpdateConcurrencyException)
    //  {
    //    if (!EmployeeExists(id))
    //    {
    //      return NotFound();
    //    }
    //    else
    //    {
    //      throw;
    //    }
    //  }

    //  return NoContent();
    //}

    [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var empIndb = await _context.Employees.FindAsync(id);
            if (empIndb == null) return NotFound();
            _context.Employees.Remove(empIndb);
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
