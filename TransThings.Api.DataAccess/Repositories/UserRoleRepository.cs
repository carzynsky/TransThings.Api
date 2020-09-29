using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class UserRoleRepository
    {
        private readonly TransThingsDbContext context;
        public UserRoleRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<UserRole> GetUserRole(User user)
        {
            return await context.UserRoles.SingleOrDefaultAsync(u => u.Id.Equals(user.UserRoleId));
        }

        public async Task<UserRole> GetUserRoleByNameAsync(string userRoleName)
        {
            var userRole = await context.UserRoles.SingleOrDefaultAsync(x => x.RoleName.ToLower().Equals(userRoleName.ToLower()));
            return userRole;
        }
        public async Task<List<UserRole>> GetAllUserRolesAsync()
        {
            var userRoles = await context.UserRoles.ToListAsync();
            return userRoles;
        }

        public async Task<UserRole> GetUserRoleByIdAsync(int id)
        {
            var userRole = await context.UserRoles.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return userRole;
        }

        public async Task AddUserRoleAsync(UserRole userRole)
        {
            await context.UserRoles.AddAsync(userRole);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserRole(UserRole userRole)
        {
            context.UserRoles.Update(userRole);
            await context.SaveChangesAsync();
        }

        public async Task RemoveUserRole(UserRole userRole)
        {
            context.UserRoles.Remove(userRole);
            await context.SaveChangesAsync();
        }
    }
}
