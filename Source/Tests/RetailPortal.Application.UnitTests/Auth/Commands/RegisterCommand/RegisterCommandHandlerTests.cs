using Moq;
using RetailPortal.Application.Auth.Commands.RegisterCommand;
using RetailPortal.Domain.Entities;
using RetailPortal.Domain.Entities.Common.ValueObjects;
using RetailPortal.Domain.Interfaces.Application.Services;
using RetailPortal.Domain.Interfaces.Infrastructure.Auth;
using RetailPortal.Domain.Interfaces.Infrastructure.Data.UnitOfWork;
using RetailPortal.Shared.Constants;

namespace RetailPortal.Unit.Auth.Commands.RegisterCommand;

public class RegisterCommandHandlerTests
{
    private readonly RegisterCommandHandler _sut;
    private readonly Mock<IUnitOfWork> _uowMock;
    private readonly Mock<IRoleService> _roleServiceMock;
    private readonly Mock<IJwtTokenGenerator> _jwtTokenGeneratorMock;
    private readonly Mock<IPasswordHasher> _passwordHasherMock;

    public RegisterCommandHandlerTests()
    {
        this._uowMock = new Mock<IUnitOfWork>();
        this._roleServiceMock = new Mock<IRoleService>();
        this._jwtTokenGeneratorMock = new Mock<IJwtTokenGenerator>();
        this._passwordHasherMock = new Mock<IPasswordHasher>();
        this._sut = new RegisterCommandHandler
        (
            this._uowMock.Object,
            this._roleServiceMock.Object,
            this._jwtTokenGeneratorMock.Object,
            this._passwordHasherMock.Object
        );
    }

    [Fact]
    public async Task Handle_ShouldReturnError_WhenEmailExists()
    {
        // Arrange
        var (user, command) = CreateUser();
        this._uowMock.Setup(u => u.UserRepository.GetUserByEmail(It.IsAny<string>())).Returns(user);

        // Act
        var result = await this._sut.Handle(command, It.IsAny<CancellationToken>());

        // Assert
        Assert.True(result.IsError);
    }

    [Fact]
    public async Task Handle_ShouldCreateUserAndReturnAuthResult_WhenEmailDoesNotExist()
    {
        // Arrange
        var (_, command) = CreateUser();
        this._uowMock.Setup(u => u.UserRepository.GetUserByEmail(It.IsAny<string>())).Returns(null as User);
        this._roleServiceMock.Setup(r => r.GetRoleByNameAsync(It.IsAny<Roles>())).ReturnsAsync(Role.Create(Roles.User.ToString(), null!));
        this._passwordHasherMock.Setup(p => p.CreatePasswordHash(It.IsAny<string>(), out It.Ref<byte[]>.IsAny, out It.Ref<byte[]>.IsAny));
        this._jwtTokenGeneratorMock.Setup(j => j.GenerateToken(It.IsAny<User>())).Returns("token");

        // Act
        var result = await this._sut.Handle(command, CancellationToken.None);

        // Assert
        Assert.False(result.IsError);
        Assert.NotNull(result.Value);
        Assert.Equal("token", result.Value.Token);
    }

    private static (User, Application.Auth.Commands.RegisterCommand.RegisterCommand) CreateUser()
    {
        var command =
            new Application.Auth.Commands.RegisterCommand.RegisterCommand("John", "Doe", "JohnDoe@email.com",
                "password");
        var user = User.Create(command.FirstName, command.LastName, command.Email, null!);

        return (user, command);
    }
}