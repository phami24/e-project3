using AptechProject3.Configuration;
using AptechProject3.DTOs.Auth;
using AptechProject3.Services;
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
        public AuthController(
            ILogger<AuthController> logger,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IOptionsMonitor<JwtConfig> optionsMonitor,
            IJwtService jwtService
            )
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtConfig = optionsMonitor.CurrentValue;
            _jwtService = jwtService;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequestDto request, string role)
        {
            if (ModelState.IsValid)
            {
                var email = await _userManager.FindByEmailAsync(request.Email);
                if (email != null)
                {
                    return BadRequest("Email is already exits !");
                }
                var newUser = new IdentityUser()
                {
                    Email = request.Email,
                    UserName = request.Email,
                };
                var isCreate = await _userManager.CreateAsync(newUser, request.Password);
                if (await _roleManager.RoleExistsAsync(role))
                {
                    if (isCreate.Succeeded)
                    {
                        var token = await _jwtService.GenerateJwtTokenAsync(newUser);
                        await _userManager.AddToRoleAsync(newUser, role);
                        return Ok(new RegistrationRespone()
                        {
                            Result = true,
                            Token = token.ToString()
                        });
                    }
                }
                else
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "This Role is not exits");
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
                    });; 
                }
            }
            return BadRequest("Invalid request payload !");

        }
    }
}
