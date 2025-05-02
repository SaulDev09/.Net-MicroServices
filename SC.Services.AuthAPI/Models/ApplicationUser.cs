using Microsoft.AspNetCore.Identity;

namespace SC.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
