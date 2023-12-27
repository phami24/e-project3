using AptechProject3.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AptechProject3.Services.ServicesImpl
{
    public class JwtService : IJwtService
    {

        private readonly JwtConfig _jwtConfig;
        private readonly UserManager<IdentityUser> _userManager;
        public JwtService(
            IOptionsMonitor<JwtConfig> optionsMonitor,
            UserManager<IdentityUser> userManager

            )
        {
            _userManager = userManager;
            _jwtConfig = optionsMonitor.CurrentValue;
        }
        public async Task<string> GenerateJwtTokenAsync(IdentityUser user)
        {
            var jwtHandler = new JwtSecurityTokenHandler();

            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);

            var roles = await _userManager.GetRolesAsync(user);
            var userRole = roles.FirstOrDefault();

            var tokenDescription = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                }),
                Expires = DateTime.UtcNow.AddHours(4),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512)
            };
            if (!string.IsNullOrEmpty(userRole))
            {
                tokenDescription.Subject.AddClaim(new Claim(ClaimTypes.Role, userRole));
            }
            else
            {
                tokenDescription.Subject.AddClaim(new Claim(ClaimTypes.Role, "User"));
            }

            var token = jwtHandler.CreateToken(tokenDescription);
            var jwtToken = jwtHandler.WriteToken(token);

            return jwtToken;
        }
    }
}
