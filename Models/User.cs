using Microsoft.AspNetCore.Identity;
namespace _3abarni_backend.Models
{
    public class User: IdentityUser
    {
        public string ProfilePic { get; set; } = String.Empty;
    }
}
