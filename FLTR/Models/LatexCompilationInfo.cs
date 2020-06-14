using Microsoft.AspNetCore.Mvc;

namespace FLTR.Models
{
    public class LatexCompilationInfo
    {
        public string Log { get; set; }

        public string CompilationOutput { get; set; }

        public bool IsDocumentChanged { get; set; }

        public bool IsCompilationSuccess { get; set; }
    }
}