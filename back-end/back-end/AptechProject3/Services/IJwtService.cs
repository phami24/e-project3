using Microsoft.AspNetCore.Identity;

namespace AptechProject3.Services
{
    public interface IJwtService
    {
        Task<string> GenerateJwtTokenAsync(IdentityUser user);
    }
}
