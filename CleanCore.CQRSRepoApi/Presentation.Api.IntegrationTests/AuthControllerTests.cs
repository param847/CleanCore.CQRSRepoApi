using Application.Common.DTOs.RequestDtos;
using Application.Common.DTOs.ResponseDtos;
using FluentAssertions;
using Shared.Models.ResponseModel;
using System.Net;
using System.Net.Http.Json;

namespace Presentation.Api.IntegrationTests
{
    public class AuthControllerTests : IClassFixture<CustomWebAppFactory>
    {
        private readonly HttpClient _client;

        public AuthControllerTests(CustomWebAppFactory factory)
            => _client = factory.CreateClient();

        [Fact]
        public async Task Register_Then_Login_ShouldWorkEndToEnd()
        {
            // 1. Register
            var regDto = new RegisterRequestDto
            {
                UserName = "intuser",
                Email = "int@user.com",
                Password = "Pass123!",
                FirstName = "Int",
                LastName = "User"
            };

            var regResp = await _client.PostAsJsonAsync("/api/auth/register", regDto);
            regResp.StatusCode.Should().Be(HttpStatusCode.Created);

            var regContent = await regResp.Content.ReadFromJsonAsync<ResponseData<AuthResponseDto>>();
            regContent!.IsSuccess.Should().BeTrue();
            regContent.Data.Token.Should().NotBeNullOrEmpty();

            // 2. Login
            var loginDto = new LoginRequestDto
            {
                UserNameOrEmail = "intuser",
                Password = "Pass123!"
            };

            var loginResp = await _client.PostAsJsonAsync("/api/auth/login", loginDto);
            loginResp.StatusCode.Should().Be(HttpStatusCode.OK);

            var loginContent = await loginResp.Content.ReadFromJsonAsync<ResponseData<AuthResponseDto>>();
            loginContent!.IsSuccess.Should().BeTrue();
            loginContent.Data.Token.Should().NotBeNullOrEmpty();
            loginContent.Data.UserName.Should().Be("intuser");
        }
    }
}