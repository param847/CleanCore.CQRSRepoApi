using Domain.Entities.Identity;

namespace Domain.Interfaces.Repositories
{
    /// <summary>
    /// User‑specific repository extending generic CRUD.
    /// </summary>
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<ApplicationUser?> GetByEmailAsync(string email);
        Task<ApplicationUser?> GetByUserNameAsync(string userName);
    }
}