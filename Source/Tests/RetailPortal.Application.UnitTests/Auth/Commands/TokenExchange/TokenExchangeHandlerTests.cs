using Moq;
using RetailPortal.Application.Auth.Commands.TokenExchange;
using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Interfaces.Application.Services;
using RetailPortal.Domain.Interfaces.Infrastructure.Auth;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;
using RetailPortal.Shared.Constants;

namespace RetailPortal.Unit.Auth.Commands.TokenExchange;

public class TokenExchangeHandlerTests
{
    private readonly TokenExchangeHandler _sut;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly Mock<IRoleService> _roleServiceMock;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;

    public TokenExchangeHandlerTests()
    {
        this._uowMock = new Mock<IUnitOfWork>();
        this._roleServiceMock = new Mock<IRoleService>();
        this._jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();

        this._sut = new TokenExchangeHandler(this._uowMock.Object, this._roleServiceMock.Object,
            this._jwtTokenGeneratorMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateUserAndReturnAuthResult_WhenUserDoesNotExist()
    {
        // Arrange
        var (user, request) = CreateUser();
        this._uowMock.Setup(u => u.UserRepository.GetUserByEmail(It.IsAny<string>())).Returns(user);
        this._jwtTokenGeneratorMock.Setup(j => j.GenerateToken(It.IsAny<User>())).Returns("token");

        // Act
        var result = await this._sut.Handle(request, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.Equal("token", result.Value.Token);
    }

    [Fact]
    public async Task Handle_ShouldReturnAuthResult_WhenUserExist()
    {
        // Arrange
        var (_, request) = CreateUser();
        this._uowMock.Setup(u => u.UserRepository.GetUserByEmail(It.IsAny<string>())).Returns(null as User);
        this._roleServiceMock.Setup(r => r.GetRoleByNameAsync(It.IsAny<Roles>()))
            .ReturnsAsync(Role.Create(Roles.User.ToString(), null!));
        this._jwtTokenGeneratorMock.Setup(j => j.GenerateToken(It.IsAny<User>())).Returns("token");

        // Act
        var result = await this._sut.Handle(request, It.IsAny<CancellationToken>());

        // Assert
        Assert.False(result.IsError);
        Assert.Equal("token", result.Value.Token);
    }

    private static (User, TokenExchangeCommand) CreateUser()
    {
        var request = new TokenExchangeCommand("JohnDoe@email.com", "John Doe", "Google");
        var name = request.Name.AsSpan();
        return (
            User.Create(name[..name.IndexOf(' ')].ToString(), name[name.IndexOf(' ')..].ToString(), request.Email,
                null!), request);
    }
}