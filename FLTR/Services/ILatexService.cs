using System.Threading.Tasks;
using FLTR.Models;
using Microsoft.AspNetCore.Mvc;

namespace FLTR.Services
{
    public interface ILatexService
    {
        Task<CompilationResult> Compile(string documentId, string userId);

        Task<FileContentResult> GetDocument(string documentId, string userId);

        void UpdateDocument(string userId, string documentId, string newText);

        Task<string> CreateDocument(string userId);
    }
}