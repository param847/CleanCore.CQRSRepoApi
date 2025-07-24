using Domain.Entities.Identity;

namespace Domain.Interfaces.Services
{
    /// <summary>
    /// Encapsulates JWT generation and validation.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Generates a signed JWT for the given user.
        /// </summary>
        string CreateToken(ApplicationUser user, IEnumerable<string> roles);
    }
}