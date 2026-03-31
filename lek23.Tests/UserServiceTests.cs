using Moq;
using lek23;

public class UserServiceTests
{
    [Fact]
    public void GetUser_ReturnsName()
    {
        var mock = new Mock<IUserRepository>();
        mock.Setup(r => r.GetUserName(1)).Returns("John");

        var service = new UserService(mock.Object);

        var result = service.GetUser(1);

        Assert.Equal("John", result);
    }

    [Fact]
    public void CreateUser_CallsSave()
    {
        var mock = new Mock<IUserRepository>();
        var service = new UserService(mock.Object);

        service.CreateUser("Alice");

        mock.Verify(r => r.Save("Alice"), Times.Once);
    }
}
