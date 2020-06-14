using System.Threading.Tasks;
using FLTR.Models;
using Microsoft.AspNetCore.Mvc;

namespace FLTR.Providers
{
    public interface IDocumentProvider
    {
        Task<LatexCompilationInfo> Compile(string userFolderName, string documentId);
        
        Task<FileContentResult> GetDocument(string userFolderPath, string documentId);

        void UpdateDocument(string userId, string documentId, string newText);

        Task<string> CreateDocument(string userId);
    }
}