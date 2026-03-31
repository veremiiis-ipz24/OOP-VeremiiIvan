using Moq;
using Xunit;

public class UserServiceTests
{
    [Fact]
    public void Register_ShouldReturnTrue_WhenUserDoesNotExist()
    {
        var repoMock = new Mock<IUserRepository>();
        var hasherMock = new Mock<IPasswordHasher>();

        repoMock.Setup(r => r.UserExists("john")).Returns(false);
        hasherMock.Setup(h => h.Hash("123")).Returns("HASH");

        var service = new UserService(repoMock.Object, hasherMock.Object);

        var result = service.Register("john", "123");

        Assert.True(result);
    }

    [Fact]
    public void Register_ShouldReturnFalse_WhenUserAlreadyExists()
    {
        var repoMock = new Mock<IUserRepository>();
        var hasherMock = new Mock<IPasswordHasher>();

        repoMock.Setup(r => r.UserExists("john")).Returns(true);

        var service = new UserService(repoMock.Object, hasherMock.Object);

        var result = service.Register("john", "123");

        Assert.False(result);
    }

    [Fact]
    public void Register_ShouldCallHashPassword()
    {
        var repoMock = new Mock<IUserRepository>();
        var hasherMock = new Mock<IPasswordHasher>();

        repoMock.Setup(r => r.UserExists(It.IsAny<string>())).Returns(false);
        hasherMock.Setup(h => h.Hash(It.IsAny<string>())).Returns("HASH");

        var service = new UserService(repoMock.Object, hasherMock.Object);

        service.Register("john", "123");

        hasherMock.Verify(h => h.Hash("123"), Times.Once);
    }

    [Fact]
    public void Register_ShouldCallAddUser()
    {
        var repoMock = new Mock<IUserRepository>();
        var hasherMock = new Mock<IPasswordHasher>();

        repoMock.Setup(r => r.UserExists(It.IsAny<string>())).Returns(false);
        hasherMock.Setup(h => h.Hash(It.IsAny<string>())).Returns("HASH");

        var service = new UserService(repoMock.Object, hasherMock.Object);

        service.Register("john", "123");

        repoMock.Verify(r => r.AddUser("john", "HASH"), Times.Once);
    }

    [Fact]
    public void UserExists_ShouldReturnTrue()
    {
        var repoMock = new Mock<IUserRepository>();
        var hasherMock = new Mock<IPasswordHasher>();

        repoMock.Setup(r => r.UserExists("john")).Returns(true);

        var service = new UserService(repoMock.Object, hasherMock.Object);

        var result = service.UserExists("john");

        Assert.True(result);
    }

    [Fact]
    public void UserExists_ShouldReturnFalse()
    {
        var repoMock = new Mock<IUserRepository>();
        var hasherMock = new Mock<IPasswordHasher>();

        repoMock.Setup(r => r.UserExists("john")).Returns(false);

        var service = new UserService(repoMock.Object, hasherMock.Object);

        var result = service.UserExists("john");

        Assert.False(result);
    }

    [Fact]
    public void HashPassword_ShouldReturnHash()
    {
        var repoMock = new Mock<IUserRepository>();
        var hasherMock = new Mock<IPasswordHasher>();

        hasherMock.Setup(h => h.Hash("123")).Returns("HASH");

        var service = new UserService(repoMock.Object, hasherMock.Object);

        var result = service.HashPassword("123");

        Assert.Equal("HASH", result);
    }

    [Fact]
    public void HashPassword_ShouldCallHasher()
    {
        var repoMock = new Mock<IUserRepository>();
        var hasherMock = new Mock<IPasswordHasher>();

        hasherMock.Setup(h => h.Hash(It.IsAny<string>())).Returns("HASH");

        var service = new UserService(repoMock.Object, hasherMock.Object);

        service.HashPassword("123");

        hasherMock.Verify(h => h.Hash("123"), Times.Once);
    }
}