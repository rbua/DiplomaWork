using System.Threading.Tasks;
using FLTR.Models;
using FLTR.Services;
using Microsoft.AspNetCore.Mvc;

namespace FLTR.Controllers
{
    [ApiController]
    [Route("Latex")]
    public class LatexController : ControllerBase
    {
        private ILatexService _latexService;
        
        public LatexController(ILatexService latexService)
        {
            _latexService = latexService;
        }
        
        [HttpPost]
        [Route("CreateEmptyDocument")]
        public Task<string> CreateEmptyDocument(string userId)
        {
            return _latexService.CreateDocument(userId);
        }
        
        [HttpGet]
        [Route("Compile")]
        public Task<CompilationResult> Compile(string userId, string documentId)
        {
            return _latexService.Compile(documentId, userId);
        }
        
        [HttpGet]
        [Route("Document")]
        public Task<FileContentResult> GetDocument(string userId, string documentId)
        {
            return _latexService.GetDocument(documentId, userId);
        }

        [HttpPost]
        [Route("UpdateDocument")]
        public void UpdateDocument(string userId, string documentId, [FromForm] string newText)
        {
            _latexService.UpdateDocument(userId, documentId, newText);
        }
    }
}
