using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace FLTR.Providers
{
    public class CommandShellProvider : ICommandShellProvider
    {
        private IHostingEnvironment _hostingEnvironment;
        
        public CommandShellProvider(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        
        public async Task<string> ExecuteCommand(string command)
        {
            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "/bin/bash",
                    Arguments = $"-c \"{command}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            
            string result = await process.StandardOutput.ReadToEndAsync();
            process.WaitForExit();
            return result;
        }

        public string GetCompilationCommand(string directoryPath, string latexFileName)
        {
            var path = Path.Combine(_hostingEnvironment.ContentRootPath, directoryPath);
            return $"cd / && cd {path} && pdflatex -interaction=nonstopmode {latexFileName}";
        }
    }
}