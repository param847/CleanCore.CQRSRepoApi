using Application.Common.DTOs.ResponseDtos;
using AutoMapper;
using Domain.Interfaces.Repositories;
using MediatR;
using Shared.Models.ResponseModel;

namespace Application.Features.Users.Queries
{
    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ResponseData<UserDto>>
    {
        private readonly IUserRepository _userRepo;
        private readonly IMapper _mapper;

        public GetUserByIdQueryHandler(IUserRepository userRepo, IMapper mapper)
        {
            _userRepo = userRepo;
            _mapper = mapper;
        }

        public async Task<ResponseData<UserDto>> Handle(GetUserByIdQuery request,
                                                       CancellationToken ct)
        {
            var user = await _userRepo.GetByIdAsync(request.Id);

            if (user == null)
                return ResponseBuilder.Failure<UserDto>(
                    statusCode: 404,
                    message: "User not found"
                );

            var dto = _mapper.Map<UserDto>(user);
            return ResponseBuilder.Success(
                data: dto,
                message: "User retrieved"
            );
        }
    }
}