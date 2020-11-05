using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class TransporterRepository
    {
        private readonly TransThingsDbContext context;

        public TransporterRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Transporter>> GetAllTransportersAsync()
        {
            var transporters = await context.Transporters.ToListAsync();
            return transporters;
        }

        public async Task<Transporter> GetTransporterByIdAsync(int id)
        {
            var transporter = await context.Transporters.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return transporter;
        }

        public async Task AddTransporterAsync(Transporter transporter)
        {
            await context.Transporters.AddAsync(transporter);
            await context.SaveChangesAsync();
        }

        public async Task UpdateTransporter(Transporter transporter)
        {
            context.Transporters.Update(transporter);
            await context.SaveChangesAsync();
        }

        public async Task RemoveTransporter(Transporter transporter)
        {
            context.Transporters.Remove(transporter);
            await context.SaveChangesAsync();
        }
    }
}
