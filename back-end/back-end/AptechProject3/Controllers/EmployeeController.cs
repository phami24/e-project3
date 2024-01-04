using AptechProject3.Comon;
using AptechProject3.DTOs.Client;
using AptechProject3.DTOs.Employee;
using AptechProject3.Models;
using AptechProject3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AptechProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        public readonly ILogger<EmployeeController> _logger;
        public readonly IEmployeeService _employeeService;
        public readonly IDepartmentService _departmentService;
        public EmployeeController(ILogger<EmployeeController> logger, IEmployeeService employeeService, IDepartmentService departmentService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _departmentService = departmentService;
        }

        [HttpGet]
        public async Task<IActionResult> Employees()
        {
            try
            {
                var employees = await _employeeService.GetAll();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateEmp(UpdateEmpRequestDto request)
        {
            try
            {
                if (request.EmployeeId != 0)
                {
                    var existingEmp = await _employeeService.GetById(request.EmployeeId);
                    if (existingEmp == null)
                    {
                        return NotFound("Employee not found");
                    }

                    var updateEmp = new Employee();
                    if (!string.IsNullOrEmpty(request.FirstName))
                    {
                        updateEmp.FirstName = request.FirstName;
                    }
                    if (!string.IsNullOrEmpty(request.LastName))
                    {
                        updateEmp.LastName = request.LastName;
                    }
                    if (!string.IsNullOrEmpty(request.Email))
                    {
                        updateEmp.Email = request.Email;
                    }
                    if (request.Age != 0 & request.Age > 18)
                    {
                        updateEmp.Email = request.Email;
                    }
                    else
                    {
                        return BadRequest("Age must be > 18");
                    }
                    if (request.DepartmentId != 0)
                    {

                        var department = await _departmentService.GetById(request.DepartmentId);
                        if (department != null)
                        {
                            updateEmp.Department = department;
                        }
                        else
                        {
                            return BadRequest("Department not found");
                        }
                    }

                    await _employeeService.Update(updateEmp);
                    return Ok(new { Employee = updateEmp, Message = "Update success!" });
                    ;
                }
                else
                {
                    return BadRequest("Invalid ClientId");
                }



            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            try
            {
                if (id != 0)
                {
                    var emp = await _employeeService.GetById(id);
                    if (emp != null)
                    {
                        await _employeeService.Delete(id);
                        return Ok();
                    }
                    return BadRequest("Employee not found");
                }
                return BadRequest("Employee not found");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
