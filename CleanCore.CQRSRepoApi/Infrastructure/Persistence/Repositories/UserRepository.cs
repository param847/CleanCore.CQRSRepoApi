using Domain.Entities.Identity;
using Domain.Interfaces.Repositories;
using Infrastructure.Persistence.Contexts;
using Infrastructure.Persistence.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories
{
    public class UserRepository : GenericRepository<ApplicationUser>, IUserRepository
    {
        public UserRepository(AppDbContext dbContext)
            : base(dbContext) { }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _dbSet
                         .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<ApplicationUser?> GetByUserNameAsync(string userName)
        {
            return await _dbSet
                         .FirstOrDefaultAsync(u => u.UserName == userName);
        }
    }
}