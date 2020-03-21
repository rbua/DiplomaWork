using System;
using System.Diagnostics;
using System.Threading.Tasks;
using FLTR.Models;

namespace FLTR.Data
{
    public class DataManager
    {
        public async Task<LatexCompilationResult> Compile(string path)
        {
            Uri.IsWellFormedUriString(path, UriKind.Absolute);
            
            var command = $"cd / && cd Users/romanbondarenko/Desktop/temporary/ && pdflatex {path}";

            await ExecuteCommand(command);
        }
        
        private async Task<string> ExecuteCommand(string command)
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
    }
}