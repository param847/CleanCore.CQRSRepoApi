using Application.Common.DTOs.ResponseDtos;
using Domain.Entities.Identity;
using Domain.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Models.ResponseModel;

namespace Application.Features.Authentication.Commands.Login
{
    public class LoginCommandHandler
        : IRequestHandler<LoginCommand, ResponseData<AuthResponseDto>>
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(UserManager<ApplicationUser> userMgr, ITokenService tokenService)
        {
            _userMgr = userMgr;
            _tokenService = tokenService;
        }

        public async Task<ResponseData<AuthResponseDto>> Handle(LoginCommand request, CancellationToken ct)
        {
            try
            {
                var dto = request.Data;

                var user = await _userMgr.FindByNameAsync(dto.UserNameOrEmail)
                           ?? await _userMgr.FindByEmailAsync(dto.UserNameOrEmail);

                if (user == null || !await _userMgr.CheckPasswordAsync(user, dto.Password))
                {
                    return ResponseBuilder.Failure<AuthResponseDto>(
                        statusCode: 401,
                        message: "Invalid username/email or password"
                    );
                }

                var roles = await _userMgr.GetRolesAsync(user);
                var token = _tokenService.CreateToken(user, roles);

                var responseDto = new AuthResponseDto
                {
                    Token = token,
                    Expires = DateTime.UtcNow.AddMinutes(60),
                    UserName = user.UserName!,
                    Email = user.Email!,
                    Roles = roles
                };

                return ResponseBuilder.Success(
                    data: responseDto,
                    message: "Login successful",
                    token: token
                );
            }
            catch (Exception ex)
            {
                return ResponseBuilder.Failure<AuthResponseDto>(
                    statusCode: 500,
                    message: "An error occurred during login",
                    ex: ex
                );
            }
        }
    }
}