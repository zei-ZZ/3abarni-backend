using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace _3abarni_backend.Models
{
    public class User: IdentityUser
    {
        public string ProfilePicPath { get; set; } = String.Empty;
        [NotMapped]
        public IFormFile ProfilePic { get; set;}
    }
}
