using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class ConfigurationRepository
    {
        private readonly TransThingsDbContext context;

        public ConfigurationRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Configuration>> GetAllConfigurationsAsync()
        {
            var configurations = await context.Configurations.ToListAsync();
            return configurations;
        }

        public async Task<Configuration> GetConfigurationByIdAsync(int id)
        {
            var configuration = await context.Configurations.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return configuration;
        }

        public async Task<Configuration> GetConfigurationByNameAsync(string name)
        {
            var configuration = await context.Configurations.SingleOrDefaultAsync(x => x.Name.ToLower().Equals(name.ToLower()));
            return configuration;
        }

        public async Task AddConfigurationAsync(Configuration configuration)
        {
            await context.Configurations.AddAsync(configuration);
            await context.SaveChangesAsync();
        }

        public async Task UpdateConfiguration(Configuration configuration)
        {
            context.Configurations.Update(configuration);
            await context.SaveChangesAsync();
        }

        public async Task RemoveConfiguration(Configuration configuration)
        {
            context.Configurations.Remove(configuration);
            await context.SaveChangesAsync();
        }
    }
}
