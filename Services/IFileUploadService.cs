namespace _3abarni_backend.Services
{
    public interface IFileUploadService
    {
        Task<string> UploadFile(IFormFile file);

    }
}
