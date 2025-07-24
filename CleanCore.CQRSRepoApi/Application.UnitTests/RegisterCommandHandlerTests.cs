using Application.Common.DTOs.RequestDtos;
using Application.Features.Authentication.Commands.Register;
using Domain.Entities.Identity;
using Domain.Interfaces.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace Application.UnitTests
{
    public class RegisterCommandHandlerTests
    {
        private readonly Mock<UserManager<ApplicationUser>> _userMgrMock;
        private readonly Mock<ITokenService> _tokenSvcMock;

        public RegisterCommandHandlerTests()
        {
            // Set up a UserManager mock
            var store = new Mock<IUserStore<ApplicationUser>>();
            _userMgrMock = new Mock<UserManager<ApplicationUser>>(store.Object, null, null, null, null, null, null, null, null);

            _tokenSvcMock = new Mock<ITokenService>();
        }

        [Fact]
        public async Task Handle_ShouldReturnSuccess_WhenCreateSucceeds()
        {
            // Arrange
            var dto = new RegisterRequestDto
            {
                UserName = "testuser",
                Email = "a@b.com",
                Password = "Pass123!",
                FirstName = "Test",
                LastName = "User"
            };

            _userMgrMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password))
                .ReturnsAsync(IdentityResult.Success);

            _userMgrMock
                .Setup(x => x.AddToRoleAsync(It.IsAny<ApplicationUser>(), "User"))
                .ReturnsAsync(IdentityResult.Success);

            _userMgrMock
                .Setup(x => x.GetRolesAsync(It.IsAny<ApplicationUser>()))
                .ReturnsAsync(new[] { "User" });

            _tokenSvcMock
                .Setup(x => x.CreateToken(It.IsAny<ApplicationUser>(), It.IsAny<string[]>()))
                .Returns("FAKE_JWT");

            var handler = new RegisterCommandHandler(_userMgrMock.Object, _tokenSvcMock.Object);

            // Act
            var response = await handler.Handle(new RegisterCommand { Data = dto }, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeTrue();
            response.StatusCode.Should().Be(201);
            response.Data.Token.Should().Be("FAKE_JWT");
            response.Data.UserName.Should().Be("testuser");
        }

        [Fact]
        public async Task Handle_ShouldReturnFailure_WhenCreateFails()
        {
            // Arrange
            var dto = new RegisterRequestDto { /* ... */ };
            var errors = new[] { new IdentityError { Description = "Bad" } };
            _userMgrMock
                .Setup(x => x.CreateAsync(It.IsAny<ApplicationUser>(), dto.Password))
                .ReturnsAsync(IdentityResult.Failed(errors));

            var handler = new RegisterCommandHandler(_userMgrMock.Object, _tokenSvcMock.Object);

            // Act
            var response = await handler.Handle(new RegisterCommand { Data = dto }, CancellationToken.None);

            // Assert
            response.IsSuccess.Should().BeFalse();
            response.StatusCode.Should().Be(400);
            response.Message.Should().Contain("Bad");
        }
    }
}