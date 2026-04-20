using BP.Application;
using BP.Application.Interfaces;
using BP.Application.Services;
using BP.Core.Users;
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BP.Tests;

public class UserServiceTests
{
    private readonly Mock<IUserRepository> _userRepositoryMock;
    private readonly Mock<IJwtOperations> _jwtOperationsMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;
    private readonly Mock<ILogger<UserService>> _loggerMock;
    private readonly UserService sut;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _jwtOperationsMock = new Mock<IJwtOperations>();
        _passwordHasherMock = new Mock<IPasswordHasher>();
        _loggerMock = new Mock<ILogger<UserService>>();

        sut = new UserService(
            _loggerMock.Object,
            _userRepositoryMock.Object,
            _jwtOperationsMock.Object,
            _passwordHasherMock.Object
        );
    }

    [Fact]
    public async Task Login_WhenUserNotFound_ReturnsError()
    {
        _userRepositoryMock
            .Setup(x => x.SearchUserByEmail("test@mail.com"))
            .ReturnsAsync(Result.Failure<User, string>("Not found"));

        var result = await sut.Login("test@mail.com", "password");

        Assert.True(result.IsFailure);
        Assert.Equal("User is null", result.Error);
    }

    [Fact]
    public async Task Login_WhenPasswordIncorrect_ReturnsError()
    {
        var user = User.Create(null, "test@mail.com", "password").Value;

        _userRepositoryMock
            .Setup(x => x.SearchUserByEmail("test@mail.com"))
            .ReturnsAsync(Result.Success<User, string>(user));

        _passwordHasherMock
            .Setup(x => x.Verify("wrongPassword", "hashedPassword"))
            .Returns(false);

        var result = await sut.Login("test@mail.com", "wrongPassword");

        Assert.True(result.IsFailure);
        Assert.Equal("Incorrect password", result.Error);
    }

    [Fact]
    public async Task Login_WhenPasswordCorrect_ReturnsToken()
    {
        var user = User.Create(null, "test@mail.com", "hashedPassword").Value;

        _userRepositoryMock
            .Setup(x => x.SearchUserByEmail("test@mail.com"))
            .ReturnsAsync(Result.Success<User, string>(user));

        _passwordHasherMock
            .Setup(x => x.Verify("password", "hashedPassword"))
            .Returns(true);

        _jwtOperationsMock
            .Setup(x => x.Generate(user))
            .Returns("fake-jwt-token");

        var result = await sut.Login("test@mail.com", "password");

        Assert.True(result.IsSuccess);
        Assert.Equal("fake-jwt-token", result.Value);
    }
    
    [Fact]
    public async Task Registration_WhenUserEmailAlreadyExists_ReturnsError()
    {
        _userRepositoryMock
            .Setup(x=>x.SearchUserByEmail("test@mail.com"))
            .ReturnsAsync(Result.Failure<User, string>("Email already exists"));

        var result = await sut.Registration("test@mail.com", "password", "testName");
        
        Assert.True(result.IsFailure);
        Assert.Equal("Email already exists", result.Error);
    }
}