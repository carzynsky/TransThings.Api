using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class DriverRepository
    {
        private readonly TransThingsDbContext context;

        public DriverRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Driver>> GetAllDriversAsync()
        {
            var drivers = await context.Drivers.Include(x => x.Transporter).ToListAsync();
            return drivers;
        }

        public async Task<Driver> GetDriverByIdAsync(int id)
        {
            var driver = await context.Drivers.Include(x => x.Transporter).FirstOrDefaultAsync(x => x.Id.Equals(id));
            return driver;
        }

        public async Task<Driver> GetDriverByPeselNumberAsync(string peselNumber)
        {
            var driver = await context.Drivers.SingleOrDefaultAsync(x => x.PeselNumber.Equals(peselNumber));
            return driver;
        }

        public async Task<List<Driver>> GetDriversByTransporter(int transporterId)
        {
            var drivers = await context.Drivers.Where(x => x.TransporterId.Equals(transporterId)).ToListAsync();
            return drivers;
        }

        public async Task AddDriverAsync(Driver driver)
        {
            await context.Drivers.AddAsync(driver);
            await context.SaveChangesAsync();
        }

        public async Task UpdateDriver(Driver driver)
        {
            context.Drivers.Update(driver);
            await context.SaveChangesAsync();
        }

        public async Task RemoveDriver(Driver driver)
        {
            context.Drivers.Remove(driver);
            await context.SaveChangesAsync();
        }
    }
}
