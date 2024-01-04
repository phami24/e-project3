using AptechProject3.Comon;
using AptechProject3.DTOs.Client;
using AptechProject3.DTOs.Department;
using AptechProject3.Models;
using AptechProject3.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AptechProject3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeparmentController : ControllerBase
    {
        public readonly ILogger<ClientController> _logger;
        public readonly IDepartmentService _departmentService;
        public readonly IEmployeeService _employeeService;
        public DeparmentController(ILogger<ClientController> logger, IDepartmentService departmentService, IEmployeeService employeeService)
        {
            _logger = logger;
            _departmentService = departmentService;
            _employeeService = employeeService;
        }

        [HttpGet]
        public async Task<IActionResult> Departments()
        {
            try
            {
                var departments = await _departmentService.GetAll();
                return Ok(departments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateDepartment(CreateDepartmentRequestDto request)
        {
            try
            {
                var exitingDepartment = await _departmentService.GetByName(request.DepartmentName);
                if (exitingDepartment == null & !string.IsNullOrEmpty(request.DepartmentName))
                {
                    var newDepartment = new Department
                    {
                        DepartmentName = request.DepartmentName
                    };

                    await _departmentService.Create(newDepartment);
                    return Ok(new { Department = newDepartment, Message = "Create success!" });
                }
                else
                {
                    return BadRequest("Create fail!");
                }



            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("AddEmp")]
        public async Task<IActionResult> AddEmployee(UpdateDepartmentRequestDto request)
        {
            try
            {
                var exitingDepartment = await _departmentService.GetByName(request.DepartmentName);
                if (exitingDepartment == null & !string.IsNullOrEmpty(request.DepartmentName))
                {
                    List<Employee> employees = new List<Employee>();
                    foreach (int id in request.EmployeeIds)
                    {
                        var employee = await _employeeService.GetById(id);
                        if (employee == null)
                        {
                            return NotFound("Employee not found");
                        }
                        employees.Add(employee);
                    }
                    await _departmentService.AddEmployee(employees, request.DepartmentId);
                    return Ok(new { Employees = employees, Message = "Add success!" });
                }
                else
                {
                    return BadRequest("Add fail!");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> UpdateDepartment(UpdateDepartmentRequestDto request)
        {
            try
            {
                if (request.DepartmentId != 0)
                {
                    var existingDepartment = await _departmentService.GetById(request.DepartmentId);
                    if (existingDepartment == null)
                    {
                        return NotFound("Departments not found");
                    }

                    var updateDepartment = new Department()
                    {
                        DepartmentId = request.DepartmentId,
                        DepartmentName = request.DepartmentName
                    };
                    if (request.EmployeeIds != null)
                    {
                        foreach (int id in request.EmployeeIds)
                        {
                            var employee = await _employeeService.GetById(id);
                            if (employee != null)
                            {
                                updateDepartment.Employees.Add(employee);
                            }
                        }
                    }
                    return Ok(new { Department = updateDepartment, Message = "Update success!" });
                    ;
                }
                else
                {
                    return BadRequest("Invalid Department");
                }



            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            try
            {
                if (id != 0)
                {
                    var client = await _departmentService.GetById(id);
                    if (client != null)
                    {
                        await _departmentService.Delete(id);
                        return Ok();
                    }
                    return BadRequest("Department not found");
                }
                return BadRequest("Department not found");

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
