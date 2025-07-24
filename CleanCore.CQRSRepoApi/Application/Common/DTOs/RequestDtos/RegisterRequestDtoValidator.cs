using FluentValidation;

namespace Application.Common.DTOs.RequestDtos
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MinimumLength(4);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
            // etc…
        }
    }
}