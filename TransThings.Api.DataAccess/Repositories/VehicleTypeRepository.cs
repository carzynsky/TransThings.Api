using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class VehicleTypeRepository
    {
        private readonly TransThingsDbContext context;

        public VehicleTypeRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<VehicleType>> GetAllVehicleTypesAsync()
        {
            var vehicleTypes = await context.VehicleTypes.ToListAsync();
            return vehicleTypes;
        }

        public async Task<VehicleType> GetVehicleTypeByIdAsync(int id)
        {
            var vehicleType = await context.VehicleTypes.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return vehicleType;
        }

        public async Task AddVehicleTypeAsync(VehicleType vehicleType)
        {
            await context.VehicleTypes.AddAsync(vehicleType);
            await context.SaveChangesAsync();
        }

        public async Task UpdateVehicleType(VehicleType vehicleType)
        {
            context.VehicleTypes.Update(vehicleType);
            await context.SaveChangesAsync();
        }

        public async Task RemoveVehicleType(VehicleType vehicleType)
        {
            context.VehicleTypes.Remove(vehicleType);
            await context.SaveChangesAsync();
        }
    }
}
