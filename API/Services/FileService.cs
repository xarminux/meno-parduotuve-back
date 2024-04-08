namespace API.Services
{
    public class FileService
    {
        private readonly string _fileDirectory;

        public FileService(string fileDirectory)
        {
            _fileDirectory = fileDirectory;
        }

        public async Task<string> SaveFileAsync(Stream fileStream, string fileName)
        {
            var filePath = Path.Combine(_fileDirectory, fileName);
            using (var file = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                await fileStream.CopyToAsync(file);
            }
            return filePath;
        }

        public async Task<Stream> GetFileAsync(string fileName)
        {
            var filePath = Path.Combine(_fileDirectory, fileName);
            var memoryStream = new MemoryStream();
            using (var file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                await file.CopyToAsync(memoryStream);
            }
            memoryStream.Position = 0;
            return memoryStream;
        }

        public bool DeleteFile(string fileName)
        {
            if (fileName == "default.jpg")
            {
                return true;
            }
            var filePath = Path.Combine(_fileDirectory, fileName);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return true;
            }
            return false;
        }
    }
}
