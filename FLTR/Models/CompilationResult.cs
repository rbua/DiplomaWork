using Microsoft.AspNetCore.Mvc;

namespace FLTR.Models
{
    public class CompilationResult
    {
        public LatexCompilationInfo CompilationInfo { get; set; }

        public FileContentResult Document { get; set; }
    }
}