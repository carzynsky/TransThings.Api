using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class UserRepository
    {
        private readonly TransThingsDbContext context;

        public UserRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            var user = await context.Users.Include(x => x.UserRole).SingleOrDefaultAsync(e => e.Login.Equals(login));
            return user;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            var user = await context.Users.Include(x => x.UserRole).SingleOrDefaultAsync(x => x.Id.Equals(id));
            return user;
        }

        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await context.Users.Include(x => x.UserRole).Include(x => x.UserRole).ToListAsync();
            return users;
        }

        public async Task<List<User>> GetAllUsersByRole(int userRoleId)
        {
            var users = await context.Users.Include(x => x.UserRole).Where(x => x.UserRoleId.Equals(userRoleId)).ToListAsync();
            return users;
        }

        public async Task AddUserAsync(User user)
        {
            await context.Users.AddAsync(user);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            context.Users.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task RemoveUser(User user)
        {
            context.Users.Remove(user);
            await context.SaveChangesAsync();
        }
    }
}
