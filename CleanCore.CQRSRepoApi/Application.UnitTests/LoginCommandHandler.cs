using Application.Common.DTOs.RequestDtos;
using Application.Features.Authentication.Commands.Login;
using Domain.Entities.Identity;
using Domain.Interfaces.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Application.UnitTests
{
    public class LoginCommandHandlerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userMgrMock;
        private readonly Mock<ITokenService> _tokenSvcMock;

        public LoginCommandHandlerTests()
        {
            var store = new Mock<IUserStore<ApplicationUser>>();
            _userMgrMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);
            _tokenSvcMock = new Mock<ITokenService>();
        }

        [Fact]
        public async Task Handle_ShouldReturnUnauthorized_WhenUserNotFound()
        {
            // Arrange
            var dto = new LoginRequestDto { UserNameOrEmail = "nosuch", Password = "x" };
            _userMgrMock.Setup(x => x.FindByNameAsync(dto.UserNameOrEmail))
                        .ReturnsAsync((ApplicationUser)null!);

            var handler = new LoginCommandHandler(_userMgrMock.Object, _tokenSvcMock.Object);

            // Act
            var response = await handler.Handle(new LoginCommand { Data = dto }, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.StatusCode.Should().Be(401);
            response.Message.Should().Contain("Invalid");
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenCredentialsValid()
        {
            // Arrange
            var dto = new LoginRequestDto { UserNameOrEmail = "user", Password = "pwd" };
            var user = new ApplicationUser { UserName = "user", Email = "e@f.com" };
            _userMgrMock.Setup(x => x.FindByNameAsync(dto.UserNameOrEmail)).ReturnsAsync(user);
            _userMgrMock.Setup(x => x.CheckPasswordAsync(user, dto.Password)).ReturnsAsync(true);
            _userMgrMock.Setup(x => x.GetRolesAsync(user)).ReturnsAsync(new[] { "User" });
            _tokenSvcMock.Setup(x => x.CreateToken(user, new[] { "User" })).Returns("JWT123");

            var handler = new LoginCommandHandler(_userMgrMock.Object, _tokenSvcMock.Object);

            // Act
            var response = await handler.Handle(new LoginCommand { Data = dto }, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.Data.Token.Should().Be("JWT123");
        }
    }
}