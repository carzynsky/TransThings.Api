using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class LoadRepository
    {
        private readonly TransThingsDbContext context;

        public LoadRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Load>> GetAllLoadsAsync()
        {
            var loads = await context.Loads.ToListAsync();
            return loads;
        }

        public async Task<List<Load>> GetLoadsByOrderAsync(int orderId)
        {
            var loads = await context.Loads.Where(x => x.OrderId.Equals(orderId)).ToListAsync();
            return loads;
        }

        public async Task<Load> GetLoadByIdAsync(int id)
        {
            var load = await context.Loads.Include(x => x.Order).SingleOrDefaultAsync(x => x.Id.Equals(id));
            return load;
        }

        public async Task AddLoadAsync(List<Load> loads)
        {
            await context.Loads.AddRangeAsync(loads);
            await context.SaveChangesAsync();
        }

        public async Task UpdateLoads(List<Load> loads)
        {
            context.Loads.UpdateRange(loads);
            await context.SaveChangesAsync();
        }

        public async Task RemoveLoad(Load load)
        {
            context.Loads.Remove(load);
            await context.SaveChangesAsync();
        }
    }
}
