
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Security.Cryptography;
using System.Threading.Tasks;
using FLTR.Models;
using Microsoft.AspNetCore.Mvc;

namespace FLTR.Providers
{
    public class DocumentProvider : IDocumentProvider
    {
        private readonly IEnvironmentProvider _environmentProvider;
        private readonly ICommandShellProvider _commandShellProvider;
        
        public DocumentProvider(IEnvironmentProvider environmentProvider, ICommandShellProvider commandShellProvider)
        {
            _environmentProvider = environmentProvider;
            _commandShellProvider = commandShellProvider;
        }

        public async Task<string> CreateDocument(string userId)
        {
            var initText = await _environmentProvider.GetInitDocument();
            return await _environmentProvider.CreateDocument(userId, initText);
        }

        public void UpdateDocument(string userId, string documentId, string newText)
        {
            _environmentProvider.UpdateDocument(userId, documentId, newText);
        }
        
        public async Task<LatexCompilationInfo> Compile(string userId, string documentId)
        {
            string latexFileName = $"{documentId}.tex";
            var userDirectoryPath = _environmentProvider.GetUserDirectoryPath(userId);
            var filePath = _environmentProvider.GetDocumentPath(userId, latexFileName);
            
            var hashBeforeCompilation = GetDocumentHash(filePath);
            
            var command = _commandShellProvider.GetCompilationCommand(userDirectoryPath, latexFileName);

            var compilationOutput = await _commandShellProvider.ExecuteCommand(command);
            var isCompilationSuccess = GetCompilationSuccess(compilationOutput);

            var hashAfterCompilation = GetDocumentHash(filePath);
            
            var logs = await GetLogs(userDirectoryPath, documentId);
            
            var compilationResult = new LatexCompilationInfo
            {
                Log = logs,
                CompilationOutput = compilationOutput,
                IsCompilationSuccess = isCompilationSuccess,
                IsDocumentChanged = CheckIsDocumentChanged(hashBeforeCompilation, hashAfterCompilation)
            };

            return compilationResult;
        }

        public async Task<FileContentResult> GetDocument(string userFolderPath, string documentId)
        {
            var filePath = _environmentProvider.GetDocumentPath(userFolderPath, $"{documentId}.tex");
            
            byte[] fileBytes = await File.ReadAllBytesAsync(filePath);

            return new FileContentResult(fileBytes, MediaTypeNames.Application.Pdf);
        }

        private byte[] GetDocumentHash(string path)
        {
            byte[] hashBytes = null;
            using (var md5 = MD5.Create())
            {
                using (var stream = File.OpenRead(path))
                {
                    hashBytes = md5.ComputeHash(stream);
                }
            }

            return hashBytes;
        }
        
        private bool CheckIsDocumentChanged(byte[] sourceHash, byte[] resultHash)
        {
            return sourceHash.SequenceEqual(resultHash);
        }

        private bool GetCompilationSuccess(string consoleOutput)
        {
            return true; // TODO: add valid console output check
        }

        private async Task<string> GetLogs(string folderPath, string documentId)
        {
            var fullPath = Path.Combine(folderPath, $"{documentId}.tex");
            using var reader = File.OpenText(fullPath);
            return await reader.ReadToEndAsync();
        }
    }
}