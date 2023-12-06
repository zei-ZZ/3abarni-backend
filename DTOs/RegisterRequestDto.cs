using System.ComponentModel.DataAnnotations;

namespace _3abarni_backend.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        [Required]
        public IFormFile? ProfilePic { get; set; }


    }
}
