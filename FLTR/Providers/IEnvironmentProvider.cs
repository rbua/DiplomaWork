using System.Threading.Tasks;

namespace FLTR.Providers
{
    public interface IEnvironmentProvider
    {
        Task<string> GetInitDocument();
    
        string GetUserDirectoryPath(string userId);
        
        string GetDocumentPath(string userId, string documentId);

        string CreateUser();

        void UpdateDocument(string userId, string documentId, string newText);
        
        Task<string> CreateDocument(string userId, string initialText);
    }
}