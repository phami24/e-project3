using Microsoft.AspNetCore.Identity;

namespace AptechProject3.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(IdentityUser user);
    }
}
