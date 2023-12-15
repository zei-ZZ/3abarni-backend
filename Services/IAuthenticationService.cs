using _3abarni_backend.DTOs;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace _3abarni_backend.Services
{
    public interface IAuthenticationService
    {
        Task<string> Register([FromForm] RegisterRequestDto request);
        Task<string> Login([FromForm] LoginRequestDto request);
    }
}
