using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace FLTR.Providers
{
    public class EnvironmentProvider : IEnvironmentProvider
    {
        private IConfigurationProvider _configuration;

        public EnvironmentProvider(IConfigurationProvider configuration)
        {
            _configuration = configuration;
        }

        public string GetUserDirectoryPath(string userId) =>
            CheckDirectoryExistence(Path.Combine(_configuration.RootFolderPath, userId));

        public string GetDocumentPath(string userId, string documentId) =>
            CheckFileExistence(Path.Combine(GetUserDirectoryPath(userId), documentId));

        public string CreateUser()
        {
            string userId = Guid.NewGuid().ToString();
            var fullPath = Path.Combine(_configuration.RootFolderPath, userId);
            
            Directory.CreateDirectory(fullPath);

            return userId;
        }

        public async Task<string> GetInitDocument()
        {
            var rootFolder = _configuration.RootFolderPath;
            string text = await File.ReadAllTextAsync(Path.Combine(rootFolder, "init.tex"));

            return text;
        }
        
        public async Task<string> CreateDocument(string userId, string initialText)
        {
            var documentId = Guid.NewGuid().ToString();
            var documentName = $"{documentId}.tex";
            var fullPath = Path.Combine(GetUserDirectoryPath(userId), documentName);

            var buffer = Encoding.UTF8.GetBytes(initialText);

            using (var fileStream = new FileStream(fullPath, FileMode.OpenOrCreate, 
                FileAccess.Write, FileShare.None, buffer.Length, true))
            {
                await fileStream.WriteAsync(buffer, 0, buffer.Length);
            }
            
            return documentId;
        }

        public void UpdateDocument(string userId, string documentId, string newText)
        {
            var fullPath = Path.Combine(GetUserDirectoryPath(userId), $"{documentId}.tex");
            var fs = File.Open(fullPath, FileMode.Truncate);
            fs.Write(Encoding.UTF8.GetBytes(newText), 0, newText.Length);
        }

        private string CheckFileExistence(string fullPath)
        {
            if (!File.Exists(fullPath))
            {
                throw new FileNotFoundException($"File with path: '{fullPath}' is not exist.");
            }

            return fullPath;
        }
        
        private string CheckDirectoryExistence(string fullPath)
        {
            if (!Directory.Exists(fullPath))
            {
                throw new FileNotFoundException($"Directory with path: '{fullPath}' is not exist.");
            }

            return fullPath;
        }
    }
}