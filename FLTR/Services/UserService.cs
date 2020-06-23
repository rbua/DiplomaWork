using System.Threading.Tasks;
using FLTR.Providers;
using MongoDB.Driver;

namespace FLTR.Services
{
    public class UserService : IUserService
    {
        private IEnvironmentProvider _environmentProvider;
        
        public UserService(IEnvironmentProvider environmentProvider)
        {
            _environmentProvider = environmentProvider;
        }
        
        public string CreateUser(string email, string passwordHash)
        {
            // TODO: add permission check and save.
            
            string connectionString = "mongodb://localhost:27017";
            MongoClient client = new MongoClient(connectionString);
            IMongoDatabase database = client.GetDatabase("fltr");
            
            return _environmentProvider.CreateUser();
        }
    }
}