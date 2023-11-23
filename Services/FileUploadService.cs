namespace _3abarni_backend.Services
{
    public class FileUploadService:IFileUploadService
    {
        private readonly string _uploadDir;
        public FileUploadService(string uploadDir) { 
            _uploadDir = uploadDir;
        }
        public async Task<string> UploadFile(IFormFile file)
        {
            if (file == null || file.Length==0) {
                throw new ArgumentException("File is nul or empty");
            }
            var fileName=Guid.NewGuid().ToString()+'_'+file.FileName;

            var filePath= Path.Combine(_uploadDir, fileName);

            await using (var stream = new FileStream(filePath, FileMode.CreateNew))
            {
                await file.CopyToAsync(stream);

            }
            return fileName;
        }
    }
}
