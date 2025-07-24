using Application.Common.DTOs.ResponseDtos;
using MediatR;
using Shared.Models.ResponseModel;

namespace Application.Features.Users.Queries
{
    public class GetUserByIdQuery
        : IRequest<ResponseData<UserDto>>
    {
        public Guid Id { get; set; }
    }
}