using System.ComponentModel.DataAnnotations;

namespace _3abarni_backend.DTOs
{
    public class LoginRequestDto
    {
        [Required]
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
