using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class LoginHistoryRepository
    {
        private readonly TransThingsDbContext context;

        public LoginHistoryRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<LoginHistory>> GetAllLoginHistoryAsync()
        {
            var loginHistories = await context.LoginHistories.Include(x => x.User).ToListAsync();
            return loginHistories;
        }

        public async Task<List<LoginHistory>> GetLoginHistoryByUserAsync(int userId)
        {
            var loginHistories = await context.LoginHistories.Where(x => x.UserId.Equals(userId)).Include(x => x.User).ToListAsync();
            return loginHistories;
        }

        public async Task<List<LoginHistory>> GetLoginHistoryByUserIdAsync(int userId)
        {
            var loginHistory = await context.LoginHistories.Where(x => x.UserId.Equals(userId)).Include(x => x.User).ToListAsync();
            return loginHistory;
        }

        public async Task AddLoginHistoryAsync(LoginHistory loginHistory)
        {
            await context.LoginHistories.AddAsync(loginHistory);
            await context.SaveChangesAsync();
        }

        public async Task UpdateLoginHistory(LoginHistory loginHistory)
        {
            context.LoginHistories.Update(loginHistory);
            await context.SaveChangesAsync();
        }

        public async Task RemoveLoginHistory(LoginHistory LoginHistory)
        {
            context.LoginHistories.Remove(LoginHistory);
            await context.SaveChangesAsync();
        }
    }
}
