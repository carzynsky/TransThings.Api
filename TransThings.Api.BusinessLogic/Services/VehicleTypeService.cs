using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class VehicleTypeService : IVehicleTypeService
    {
        private readonly IUnitOfWork unitOfWork;

        public VehicleTypeService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<VehicleType>> GetAllVehicleTypes()
        {
            var vehicleTypes = await unitOfWork.VehicleTypeRepository.GetAllVehicleTypesAsync();
            return vehicleTypes;
        }

        public async Task<VehicleType> GetVehicleTypeById(int id)
        {
            var vehicleType = await unitOfWork.VehicleTypeRepository.GetVehicleTypeByIdAsync(id);
            return vehicleType;
        }

        public async Task<GenericResponse> AddVehicleType(VehicleType vehicleType)
        {
            if (vehicleType == null)
                return new GenericResponse(false, "Vehicle type has not been provided.");

            if (string.IsNullOrEmpty(vehicleType.TypeName))
                return new GenericResponse(false, "Vehicle type name has not been provided.");

            try
            {
                await unitOfWork.VehicleTypeRepository.AddVehicleTypeAsync(vehicleType);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "New vehicle type has been created.");
        }

        public async Task<GenericResponse> RemoveVehicleType(int id)
        {
            var vehicleTypeToRemove = await unitOfWork.VehicleTypeRepository.GetVehicleTypeByIdAsync(id);
            if (vehicleTypeToRemove == null)
                return new GenericResponse(false, $"Vehicle type  with id={id} does not exist.");

            try
            {
                await unitOfWork.VehicleTypeRepository.RemoveVehicleType(vehicleTypeToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Vehicle type has been removed.");
        }

        public async Task<GenericResponse> UpdateVehicleType(VehicleType vehicleType, int id)
        {
            if (vehicleType == null)
                return new GenericResponse(false, "No vehicle type has been provided.");

            var vehicleTypeToUpdate = await unitOfWork.VehicleTypeRepository.GetVehicleTypeByIdAsync(id);
            if (vehicleTypeToUpdate == null)
                return new GenericResponse(false, $"Vehicle type  with id={id} does not exist.");

            if (string.IsNullOrEmpty(vehicleType.TypeName))
                return new GenericResponse(false, "Vehicle type name has not been provided.");

            vehicleTypeToUpdate.TypeName = vehicleType.TypeName;

            try
            {
                await unitOfWork.VehicleTypeRepository.UpdateVehicleType(vehicleTypeToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Vehicle type has been updated.");
        }
    }
}
