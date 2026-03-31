public interface IUserRepository
{
    void AddUser(string username, string passwordHash);
    bool UserExists(string username);
}