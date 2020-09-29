using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IUnitOfWork unitOfWork;

        public VehicleService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            var vehicles = await unitOfWork.VehicleRepository.GetAllVehiclesAsync();
            return vehicles;
        }

        public async Task<Vehicle> GetVehicleById(int id)
        {
            var vehicle = await unitOfWork.VehicleRepository.GetVehicleByIdAsync(id);
            return vehicle;
        }

        public async Task<GenericResponse> AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                return new GenericResponse(false, "Vehicle has not been provided.");

            if (vehicle.ProductionYear.Length != 4)
                return new GenericResponse(false, "Incorrect production year has been provided.");

            try
            {
                await unitOfWork.VehicleRepository.AddVehicleAsync(vehicle);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Vehicle has been created.");
        }

        public async Task<GenericResponse> RemoveVehicle(int id)
        {
            var vehicleToRemove = await unitOfWork.VehicleRepository.GetVehicleByIdAsync(id);
            if (vehicleToRemove == null)
                return new GenericResponse(false, $"Vehicle with id={id} does not exist.");

            try
            {
                await unitOfWork.VehicleRepository.RemoveVehicle(vehicleToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Vehicle has been removed.");
        }

        public async Task<GenericResponse> UpdateVehicle(Vehicle vehicle, int id)
        {
            if (vehicle == null)
                return new GenericResponse(false, "Vehicle has not been provided.");

            var vehicleToUpdate = await unitOfWork.VehicleRepository.GetVehicleByIdAsync(id);
            if (vehicleToUpdate == null)
                return new GenericResponse(false, $"Vehicle with id={id} does not exist.");

            if (vehicle.ProductionYear.Length != 4)
                return new GenericResponse(false, "Incorrect production year has been provided.");

            vehicleToUpdate.Brand = vehicle.Brand;
            vehicleToUpdate.LoadingCapacity = vehicle.LoadingCapacity;
            vehicleToUpdate.Model = vehicle.Model;
            vehicleToUpdate.ProductionYear = vehicle.ProductionYear;
            vehicleToUpdate.Trailer = vehicle.Trailer;
            vehicleToUpdate.TransporterId = vehicle.TransporterId;
            vehicleToUpdate.VehicleTypeId = vehicle.VehicleTypeId;

            try
            {
                await unitOfWork.VehicleRepository.UpdateVehicle(vehicleToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Vehicle has been updated.");
        }

        public async Task<List<Vehicle>> GetVehiclesByTransporter(int transporterId)
        {
            var vehicles = await unitOfWork.VehicleRepository.GetVehiclesByTransporter(transporterId);
            return vehicles;
        }
    }
}
