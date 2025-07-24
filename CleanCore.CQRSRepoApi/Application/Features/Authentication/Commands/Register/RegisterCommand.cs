using Application.Common.DTOs.RequestDtos;
using Application.Common.DTOs.ResponseDtos;
using MediatR;
using Shared.Models.ResponseModel;

namespace Application.Features.Authentication.Commands.Register
{
    public class RegisterCommand : IRequest<ResponseData<AuthResponseDto>>
    {
        public RegisterRequestDto Data { get; set; } = null!;
    }
}