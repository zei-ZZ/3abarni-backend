using _3abarni_backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http; // Add this namespace for IFormFile
using System;
using System.Threading.Tasks;

namespace _3abarni_backend.Controllers
{
    public class FileController : Controller
    {
        private readonly IFileUploadService _fileUploadService;
        private readonly IConfiguration _configuration;

        public FileController(IFileUploadService fileUploadService, IConfiguration configuration)
        {
            _fileUploadService = fileUploadService;
            _configuration = configuration;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            try
            {
                var uploadedFileName = await _fileUploadService.UploadFile(_configuration.GetSection("FileUpload:ProfilePictures").Value, file);
                return Ok(new { FileName = uploadedFileName });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }
    }
}
