using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class WarehouseRepository
    {
        private readonly TransThingsDbContext context;

        public WarehouseRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Warehouse>> GetAllWarehousesAsync()
        {
            var warehouses = await context.Warehouses.ToListAsync();
            return warehouses;
        }

        public async Task<Warehouse> GetWarehouseByIdAsync(int id)
        {
            var warehouse = await context.Warehouses.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return warehouse;
        }

        public async Task AddWarehouseAsync(Warehouse warehouse)
        {
            await context.Warehouses.AddAsync(warehouse);
            await context.SaveChangesAsync();
        }

        public async Task UpdateWarehouse(Warehouse warehouse)
        {
            context.Warehouses.Update(warehouse);
            await context.SaveChangesAsync();
        }

        public async Task RemoveWarehouse(Warehouse warehouse)
        {
            context.Warehouses.Remove(warehouse);
            await context.SaveChangesAsync();
        }
    }
}
