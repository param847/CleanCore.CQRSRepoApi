using Application.Common.DTOs.RequestDtos;
using Application.Common.DTOs.ResponseDtos;
using MediatR;
using Shared.Models.ResponseModel;

namespace Application.Features.Authentication.Commands.Login
{
    public class LoginCommand : IRequest<ResponseData<AuthResponseDto>>
    {
        public LoginRequestDto Data { get; set; } = null!;
    }
}