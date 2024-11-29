using Arahk.Application.User.Login;
using Arahk.Application.User.Register;
using Arahk.Domain.Identity.Entities;
using Arahk.Domain.Identity.Repositories;
using Arahk.Domain.Identity.ValueObjects;
using Moq;

namespace Arahk.Application.Test;

public class UserUnitTest
{
    [Fact]
    public async Task UserLoginTest()
    {
        // Arrange
        const string username = "test0001";
        var mockUser = new UserEntity(Guid.NewGuid(), username, "test!1234", "test@test.com")
        {
            HashedPassword =
            {
                Value = "cv3Yv/nf3F966cHnvgYXtJPRceJbziQ3U+xq6hH0SNIrsvo5EI6/UJ9Orecnzu3t"
            }
        };

        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(p => p.GetByUserNameAsync(It.IsAny<UsernameValueObject>())).ReturnsAsync(mockUser);
        
        var request = new UserLoginRequest
        {
            Username = "test0001",
            Password = "test!1234"
        };
        
        var handler = new UserLoginHandler(userRepositoryMock.Object);
        
        // Action
        var user = await handler.Handle(request);
        
        // Assert
        Assert.NotEqual(Guid.Empty, user.Id);
    }
    
    [Fact]
    public async Task UserRegisterTest()
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<UsernameValueObject>())).ReturnsAsync(false);

        var request = new UserRegisterRequest
        {
            Username = "test0001",
            Password = "test!1234",
            Email = "test@email.com",
        };
        
        var handler = new UserRegisterHandler(userRepositoryMock.Object);
        
        // Action
        var user = await handler.Handle(request);
        
        // Assert
        Assert.NotEqual(Guid.Empty, user.Id);
        Assert.Equal(user.Username.Value, request.Username);
        Assert.NotEmpty(user.HashedPassword.Value);
        Assert.NotEqual(user.HashedPassword.Value, request.Password);
        Assert.Equal(user.Email.Value, request.Email);
    }
    
    [Theory]
    [InlineData("","","")]
    [InlineData("test0001","","")]
    [InlineData("test0001","test!1234","")]
    [InlineData("test0001234567890123456789","test!1234","test@email.com")]
    [InlineData("123456789","test!1234","test@email.com")]
    [InlineData("test0001!!!","test!1234","test@email.com")]
    [InlineData("test0001","test1234","test@email.com")]
    [InlineData("test0001","test1234","testemail.com")]
    public async Task UserRegisterInvalidTest(string username, string password, string email)
    {
        // Arrange
        var userRepositoryMock = new Mock<IUserRepository>();
        userRepositoryMock.Setup(p => p.ExistsAsync(It.IsAny<UsernameValueObject>())).ReturnsAsync(() => false);;

        var request = new UserRegisterRequest
        {
            Username = username,
            Password = password,
            Email = email
        };
        
        var handler = new UserRegisterHandler(userRepositoryMock.Object);
        
        // Action
        await Assert.ThrowsAsync<InvalidDataException>(() => handler.Handle(request));
    }
}