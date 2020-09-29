using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.DataAccess.Repositories
{
    public class VehicleRepository
    {
        private readonly TransThingsDbContext context;

        public VehicleRepository(TransThingsDbContext context)
        {
            this.context = context;
        }

        public async Task<List<Vehicle>> GetAllVehiclesAsync()
        {
            var vehicles = await context.Vehicles.ToListAsync();
            return vehicles;
        }

        public async Task<List<Vehicle>> GetVehiclesByTransporter(int transporterId)
        {
            var vehicles = await context.Vehicles.Where(x => x.TransporterId.Equals(transporterId)).ToListAsync();
            return vehicles;
        }

        public async Task<Vehicle> GetVehicleByIdAsync(int id)
        {
            var vehicle = await context.Vehicles.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return vehicle;
        }

        public async Task AddVehicleAsync(Vehicle vehicle)
        {
            await context.Vehicles.AddAsync(vehicle);
            await context.SaveChangesAsync();
        }

        public async Task UpdateVehicle(Vehicle vehicle)
        {
            context.Vehicles.Update(vehicle);
            await context.SaveChangesAsync();
        }

        public async Task RemoveVehicle(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
            await context.SaveChangesAsync();
        }
    }
}
