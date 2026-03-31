public class UserService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;

    public UserService(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public bool Register(string username, string password)
    {
        if (_userRepository.UserExists(username))
            return false;

        var hash = _passwordHasher.Hash(password);
        _userRepository.AddUser(username, hash);

        return true;
    }

    public bool UserExists(string username)
    {
        return _userRepository.UserExists(username);
    }

    public string HashPassword(string password)
    {
        return _passwordHasher.Hash(password);
    }
}