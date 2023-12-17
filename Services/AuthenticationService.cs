using _3abarni_backend.DTOs;
using _3abarni_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Asn1.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;
using System.Web;

namespace _3abarni_backend.Services
{
    public class AuthenticationService:IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IFileUploadService _fileUploadService;
        private readonly IEmailSender _emailSender;

        public AuthenticationService(UserManager<User> userManager, IConfiguration configuration, IFileUploadService fileUploadService, IEmailSender emailSender)
        {
            _userManager = userManager;
            _configuration = configuration;
            _fileUploadService = fileUploadService;
            _emailSender = emailSender;
        }

        public async Task <string> Register([FromForm] RegisterRequestDto request)
        {
            if(request.Password!= request.PasswordConfirmation)
            {
                throw new ArgumentException("password and password confirmation don't match");
            }
            // using var transaction = new TransactionScope();
             using (var tx = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
              {
                    try
                    {
                    var userByEmail = await _userManager.FindByEmailAsync(request.Email);
                    if (userByEmail is not null)
                    {
                        throw new ArgumentException($"User with email {request.Email} exists.");
                    }

                    User user = new()
                    {
                        Email = request.Email,
                        UserName = request.Username,

                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    var result = await _userManager.CreateAsync(user, request.Password);

                    if (!result.Succeeded)
                    {
                        throw new ArgumentException($"Unable to register user {request.Username} errors: {GetErrorsText(result.Errors)}");
                    }
                    if (request.ProfilePic is not null)
                    {
                        user.ProfilePicPath = await _fileUploadService.UploadFile(_configuration.GetSection("FileUpload:ProfilePictures").Value, request.ProfilePic);
                    }
                    else
                    {
                        user.ProfilePicPath = Path.Combine(_configuration.GetSection("FileUpload:ProfilePictures").Value, "default.jpg");
                    }
                    var userFromDb = await _userManager.FindByNameAsync(user.UserName);
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    
                    var emailBody = "please verify your email address <a href=\" #URL \"> Click here </a> ";


                    /*var uriBuilder= new UriBuilder("https://localhost:7225/confirmemail");
                    var query = HttpUtility.ParseQueryString(uriBuilder.Query);
                    query["token"]=token;
                    query["userid"] = userFromDb.Id;
                    uriBuilder.Query = query.ToString();
                    var urlString = uriBuilder.ToString();
                    var senderEmail = _configuration["Email:SenderEmail"];
                    await _emailSender.SendEmailAsync(senderEmail, userFromDb.Email, "confirm your email address", urlString);
                    */
                    await _emailSender.SendEmailAsync( "zeinebba12@gmail.com", "hi", "hi");
                 
                    await _userManager.UpdateAsync(user);
                    return await Login(new LoginRequestDto { Email = request.Email, Password = request.Password });
                }
            catch (Exception ex)
            {
                throw new Exception("Registration failed", ex);
            }
            }

        }
        
        public async Task<string> Login(LoginRequestDto request)
        {
         
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
            {
                throw new ArgumentException($"Unable to authenticate user {request.Email}");
            }

            var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.UserName),
            new(ClaimTypes.Email, user.Email),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

            var token = GetToken(authClaims);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private JwtSecurityToken GetToken(IEnumerable<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(3),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256));

            return token;
        }

        private string GetErrorsText(IEnumerable<IdentityError> errors)
        {
            return string.Join(", ", errors.Select(error => error.Description).ToArray());
        }
    }
}
