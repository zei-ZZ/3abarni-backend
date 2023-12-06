namespace _3abarni_backend.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFile(string uploadDir,IFormFile file);

    }
}
