using _3abarni_backend.DTOs;
using System.IdentityModel.Tokens.Jwt;

namespace _3abarni_backend.Services
{
    public interface IAuthenticationService
    {
        Task<string> Register(RegisterRequestDto request);
        Task<string> Login(LoginRequestDto request);
    }
}
