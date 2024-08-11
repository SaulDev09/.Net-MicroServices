using SC.Services.AuthAPI.Models;

namespace SC.Services.AuthAPI.Service.IService
{
    public interface IJwtTokenGenerator
    {
        //string GenerateToken(ApplicationUser applicationUser, IEnumerable<string> roles);
        string GenerateToken(ApplicationUser applicationUser);
    }
}
