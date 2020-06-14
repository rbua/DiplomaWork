using System.Threading.Tasks;
using FLTR.Models;
using FLTR.Providers;
using Microsoft.AspNetCore.Mvc;

namespace FLTR.Services
{
    public class LatexService : ILatexService
    {
        private IDocumentProvider _documentProvider;
        
        public LatexService(IDocumentProvider documentProvider)
        {
            _documentProvider = documentProvider;
        }

        public async Task<string> CreateDocument(string userId)
        {
            return await _documentProvider.CreateDocument(userId);
        }
        
        public async  Task<CompilationResult> Compile(string documentId, string userId)
        {
            var compilationInfo = await _documentProvider.Compile(userId, documentId);
            FileContentResult document = null;

            if (compilationInfo.IsCompilationSuccess)
            {
                document = await _documentProvider.GetDocument(userId, documentId);
            }

            return new CompilationResult
            {
                CompilationInfo = compilationInfo,
                Document = document
            };
        }

        public void UpdateDocument(string userId, string documentId, string newText)
        {
            _documentProvider.UpdateDocument(userId, documentId, newText);
        }
        
        public async Task<FileContentResult> GetDocument(string documentId, string userId)
        {
            return await _documentProvider.GetDocument(userId, documentId);
        }
    }
}