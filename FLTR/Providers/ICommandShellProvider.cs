using System.Threading.Tasks;

namespace FLTR.Providers
{
    public interface ICommandShellProvider
    {
        Task<string> ExecuteCommand(string command);

        string GetCompilationCommand(string directoryPath, string latexFileName);
    }
}