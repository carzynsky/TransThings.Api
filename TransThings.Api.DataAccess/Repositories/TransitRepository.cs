using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class TransitRepository
    {
        private readonly TransThingsDbContext context;

        public TransitRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Transit>> GetAllTransitsAsync()
        {
            var transits = await context.Transits.Include(x => x.PaymentForm).ToListAsync();
            return transits;
        }

        public async Task<Transit> GetTransitByIdAsync(int id)
        {
            var transit = await context.Transits.Include(x => x.PaymentForm).SingleOrDefaultAsync(x => x.Id.Equals(id));
            return transit;
        }

        public async Task AddTransitAsync(Transit transit)
        {
            await context.Transits.AddAsync(transit);
            await context.SaveChangesAsync();
        }

        public async Task UpdateTransit(Transit transit)
        {
            context.Transits.Update(transit);
            await context.SaveChangesAsync();
        }

        public async Task RemoveTransit(Transit transit)
        {
            context.Transits.Remove(transit);
            await context.SaveChangesAsync();
        }
    }
}
