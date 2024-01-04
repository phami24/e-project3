using AptechProject3.Comon;
using AptechProject3.Configuration;
using AptechProject3.DTOs.Auth;
using AptechProject3.Models;
using AptechProject3.Services;
using AptechProject3.Services.ServicesImpl;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AptechProject3.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtConfig _jwtConfig;
        private readonly IJwtService _jwtService;
        private readonly IDepartmentService _departmentService;
        private readonly IUnitOfWork _unitOfWork;
        public AuthController(
            ILogger<AuthController> logger,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            IJwtService jwtService,
            IUnitOfWork unitOfWork,
            IDepartmentService departmentService
            )
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _jwtService = jwtService;
            _departmentService = departmentService;
            _unitOfWork = unitOfWork;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var email = await _userManager.FindByEmailAsync(request.Email);
                if (email != null)
                {
                    return BadRequest("Email is already exits !");
                }
                var newUser = new Client()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    UserName = request.Email,
                };
                var isCreate = await _userManager.CreateAsync(newUser, request.Password);
                if (isCreate.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, "User");
                    var token = await _jwtService.GenerateJwtTokenAsync(newUser);
                    await _unitOfWork.Clients.Add(newUser);
                    await _unitOfWork.CompleteAsync();
                    return Ok(new ClientRegistrationRespone()
                    {
                        Result = true,
                        Token = token.ToString()
                    });
                }

                return BadRequest(isCreate.Errors.Select(x => x.Description).ToList());

            }

            return BadRequest("Invalid request payload !");

        }
        [HttpPost]
        [Route("EmpRegister")]
        public async Task<IActionResult> EmpRegister([FromBody] EmployeeRegistrationRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var email = await _userManager.FindByEmailAsync(request.Email);
                if (email != null)
                {
                    return BadRequest("Email is already exits !");
                }
                var newEmp = new Employee()
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    UserName = request.Email,
                };
                Department? department = await _departmentService.GetById(request.DepartmentId);
                if (department != null)
                {
                    newEmp.Department = department;
                    List<Employee> employees = new List<Employee>();
                    employees.Add(newEmp);
                    await _departmentService.AddEmployee(employees, request.DepartmentId);
                }
                else
                {
                    return BadRequest("Department not found");
                }
                var isCreate = await _userManager.CreateAsync(newEmp, request.Password);
                if (isCreate.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newEmp, "User");
                    await _unitOfWork.Employees.Add(newEmp);
                    await _unitOfWork.CompleteAsync();
                    var token = await _jwtService.GenerateJwtTokenAsync(newEmp);
                    return Ok(new ClientRegistrationRespone()
                    {
                        Result = true,
                        Token = token.ToString()
                    });
                }

                return BadRequest(isCreate.Errors.Select(x => x.Description).ToList());

            }

            return BadRequest("Invalid request payload !");

        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(request.Email);
                if (existingUser == null)
                {
                    return BadRequest("Invalid authentication");
                }
                var isPasswordValid = await _userManager.CheckPasswordAsync(existingUser, request.Password);
                if (isPasswordValid)
                {
                    var token = await _jwtService.GenerateJwtTokenAsync(existingUser);
                    return Ok(new LoginRespone()
                    {
                        Result = true,
                        Token = token.ToString()
                    }); ;
                }
            }
            return BadRequest("Invalid request payload !");
        }
    }
}
