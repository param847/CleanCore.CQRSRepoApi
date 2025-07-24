namespace Application.Common.DTOs.ResponseDtos
{
    public class AuthResponseDto
    {
        public string Token { get; set; } = null!;
        public DateTime Expires { get; set; }
        public string UserName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public IEnumerable<string> Roles { get; set; } = Array.Empty<string>();
    }
}