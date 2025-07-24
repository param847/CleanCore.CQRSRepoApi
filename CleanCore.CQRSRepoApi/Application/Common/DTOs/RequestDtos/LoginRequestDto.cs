namespace Application.Common.DTOs.RequestDtos
{
    public class LoginRequestDto
    {
        public string UserNameOrEmail { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}