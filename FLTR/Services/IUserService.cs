namespace FLTR.Services
{
    public interface IUserService
    {
        string CreateUser(string email, string passwordHash);
    }
}