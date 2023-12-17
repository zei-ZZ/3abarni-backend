using _3abarni_backend.DTOs;
using _3abarni_backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace _3abarni_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto request)
        {
            var response = await _authenticationService.Login(request);

            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] RegisterRequestDto request)
        {
            var response = await _authenticationService.Register(request);

            return Ok(response);
        }
        [Route ("confirmemail")]
        [HttpGet]
        public async Task <IActionResult> ConfirmEmail(string userId, string token)
        {
           
            var response =await _authenticationService.ConfirmEmail(userId,token) ;
            return Ok();
        }
    }
}
