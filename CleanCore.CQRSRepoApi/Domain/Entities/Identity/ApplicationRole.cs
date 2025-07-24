using Microsoft.AspNetCore.Identity;

namespace Domain.Entities.Identity
{
    /// <summary>
    /// Extends IdentityRole for application‑specific roles.
    /// </summary>
    public class ApplicationRole : IdentityRole<Guid>
    {
        public string Description { get; set; } = string.Empty;
    }
}