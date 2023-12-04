﻿using _3abarni_backend.DTOs;
using _3abarni_backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Transactions;

namespace _3abarni_backend.Services
{
    public class AuthenticationService:IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IFileUploadService _fileUploadService;
        public AuthenticationService (UserManager<User> userManager, IConfiguration configuration, IFileUploadService fileUploadService)
        {
            _userManager = userManager;
            _configuration = configuration;
            _fileUploadService = fileUploadService;
        }

        public async Task <string> Register([FromForm] RegisterRequestDto request)
        {
           // using var transaction = new TransactionScope();
            /*try
            { */

            var userByEmail= await _userManager.FindByNameAsync(request.Email);
            if(userByEmail is not null) {
                throw new ArgumentException($"User with email {request.Email} exists.");
            }

            User user = new() {
                Email = request.Email,
                UserName = request.Username,

                SecurityStamp = Guid.NewGuid().ToString()
            };
            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                throw new ArgumentException($"Unable to register user {request.Username} errors: {GetErrorsText(result.Errors)}");
            }
            if( request.ProfilePic is not null ) {
                    user.ProfilePicPath = await _fileUploadService.UploadFile(_configuration.GetSection("FileUpload:ProfilePictures").Value,request.ProfilePic);
                }
            else
            {
                    user.ProfilePicPath = Path.Combine(_configuration.GetSection("FileUpload:ProfilePictures").Value, "default.jpg");
            }
                await _userManager.UpdateAsync(user);
                //transaction.Complete();
                return await Login(new LoginRequestDto { Email = request.Email, Password = request.Password });
            /*}
            catch (Exception ex) {
                transaction.Dispose();  // Rollback the transaction
                throw new Exception("Registration failed", ex);
            }*/

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
