using Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    /// <summary>
    /// Extends IdentityUser for application‑specific properties.
    /// </summary>
    public class ApplicationUser : IdentityUser<Guid>
    {
        // Example of additional profile fields:
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // e.g. status (active, suspended, etc.)
        public UserStatus Status { get; set; } = UserStatus.Active;
    }
}