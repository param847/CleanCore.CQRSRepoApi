using Application.Common.DTOs.ResponseDtos;
using Domain.Entities.Identity;
using Domain.Interfaces.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shared.Models.ResponseModel;

namespace Application.Features.Authentication.Commands.Register
{
    public class RegisterCommandHandler
        : IRequestHandler<RegisterCommand, ResponseData<AuthResponseDto>>
    {
        private readonly UserManager<ApplicationUser> _userMgr;
        private readonly ITokenService _tokenService;

        public RegisterCommandHandler(UserManager<ApplicationUser> userMgr, ITokenService tokenService)
        {
            _userMgr = userMgr;
            _tokenService = tokenService;
        }

        public async Task<ResponseData<AuthResponseDto>> Handle(RegisterCommand request, CancellationToken ct)
        {
            try
            {
                var dto = request.Data;

                var user = new ApplicationUser
                {
                    UserName = dto.UserName,
                    Email = dto.Email,
                    FirstName = dto.FirstName,
                    LastName = dto.LastName,
                    Status = Domain.Enums.UserStatus.Active
                };

                var result = await _userMgr.CreateAsync(user, dto.Password);
                if (!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                    return ResponseBuilder.Failure<AuthResponseDto>(
                        statusCode: 400,
                        message: errors
                    );
                }

                await _userMgr.AddToRoleAsync(user, "User");

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
                    message: "Registration successful",
                    token: token,
                    statusCode: 201
                );
            }
            catch (Exception ex)
            {
                return ResponseBuilder.Failure<AuthResponseDto>(
                    statusCode: 500,
                    message: "An error occurred during registration",
                    ex: ex
                );
            }
        }
    }
}