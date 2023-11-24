namespace _3abarni_backend.Services
{
    public class FileUploadService:IFileUploadService
    {
        public FileUploadService()
        {
        }

        public async Task<string> UploadFile(string uploadDir,IFormFile file)
        {
            if (file == null || file.Length==0) {
                throw new ArgumentException("File is nul or empty");
            }
            var fileName=Guid.NewGuid().ToString()+'_'+file.FileName;

            var filePath= Path.Combine(uploadDir, fileName);

            await using (var stream = new FileStream(filePath, FileMode.CreateNew))
            {
                await file.CopyToAsync(stream);

            }
            return fileName;
        }
    }
}
