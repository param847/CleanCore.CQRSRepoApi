using Application.Common.DTOs.ResponseDtos;
using Application.Features.Users.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.Models.ResponseModel;

namespace Presentation.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator) => _mediator = mediator;

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ResponseData<UserDto>>> GetById(Guid id)
        {
            var response = await _mediator.Send(new GetUserByIdQuery { Id = id });
            return StatusCode(response.StatusCode, response);
        }
    }
}