using _3abarni_backend.Services;
using Microsoft.AspNetCore.Mvc;

namespace _3abarni_backend.Controllers
{
    public class FileController : Controller
    {
        private readonly  FileUploadService _fileUploadService;
      

        public FileController(FileUploadService fileUploadService)
        {
            _fileUploadService = fileUploadService;
        }

        [HttpPost("upload")]
        public async Task<IActionResult>UploadFile(IFormFile file)
        {
            try
            {
                var uploadedFileName = await _fileUploadService.UploadFile(file);
                return Ok(new { FileName = uploadedFileName });

            }
            catch (Exception ex)
            {
                return BadRequest(new{Error=ex.Message });
            }
        }
    }
}
