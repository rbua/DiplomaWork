using Microsoft.AspNetCore.Mvc;

namespace FLTR.Models
{
    public class LatexCompilationResult
    {
        public string Log { get; set; }
        public FileStreamResult PdfResult { get; set; }
    }
}