using FLTR.Services;
using Microsoft.AspNetCore.Mvc;

namespace FLTR.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LatexController : ControllerBase
    {
        private ILatexService _latexService;
        
        public LatexController(ILatexService latexService)
        {
            _latexService = latexService;
        }
        
        [HttpGet]
        public FileContentResult Get()
        {
            
            return new FileStreamResult(workStream, "application/pdf");
        }
    }
}
