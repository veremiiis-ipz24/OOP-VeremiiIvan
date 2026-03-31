namespace lek23;

public interface IUserRepository
{
    string GetUserName(int id);
    void Save(string name);
}

public class UserService
{
    private readonly IUserRepository _repository;

    public UserService(IUserRepository repository)
    {
        _repository = repository;
    }

    public string GetUser(int id)
    {
        return _repository.GetUserName(id);
    }

    public void CreateUser(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Invalid name");

        _repository.Save(name);
    }
}
