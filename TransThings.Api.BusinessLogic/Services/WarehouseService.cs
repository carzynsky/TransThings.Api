using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Constants;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IUnitOfWork unitOfWork;

        public WarehouseService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<Warehouse>> GetAllWarehouses()
        {
            var warehouses = await unitOfWork.WarehouseRepository.GetAllWarehousesAsync();
            return warehouses;
        }

        public async Task<Warehouse> GetWarehouseById(int id)
        {
            var warehouse = await unitOfWork.WarehouseRepository.GetWarehouseByIdAsync(id);
            return warehouse;

        }

        public async Task<GenericResponse> AddWarehouse(Warehouse warehouse)
        {
            if (warehouse == null)
                return new GenericResponse(false, WarehouseResponseMessage.WarehouseDataNotProvided);

            if (string.IsNullOrEmpty(warehouse.Name) || string.IsNullOrEmpty(warehouse.StreetAddress)
                || string.IsNullOrEmpty(warehouse.ZipCode) || string.IsNullOrEmpty(warehouse.City))
                return new GenericResponse(false, WarehouseResponseMessage.LocationDataNotProvided);

            try
            {
                await unitOfWork.WarehouseRepository.AddWarehouseAsync(warehouse);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, WarehouseResponseMessage.WarehouseCreated);
        }

        public async Task<GenericResponse> RemoveWarehouse(int id)
        {
            var warehouseToRemove = await unitOfWork.WarehouseRepository.GetWarehouseByIdAsync(id);
            if (warehouseToRemove == null)
                return new GenericResponse(false, WarehouseResponseMessage.WarehouseWithGivenIdNotExists);

            try
            {
                await unitOfWork.WarehouseRepository.RemoveWarehouse(warehouseToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, WarehouseResponseMessage.WarehouseRemoved);
        }

        public async Task<GenericResponse> UpdateWarehouse(Warehouse warehouse, int id)
        {
            if (warehouse == null)
                return new GenericResponse(false, WarehouseResponseMessage.WarehouseDataNotProvided);

            var warehouseToUpdate = await unitOfWork.WarehouseRepository.GetWarehouseByIdAsync(id);
            if (warehouseToUpdate == null)
                return new GenericResponse(false, WarehouseResponseMessage.WarehouseWithGivenIdNotExists);

            if (string.IsNullOrEmpty(warehouse.Name) || string.IsNullOrEmpty(warehouse.StreetAddress)
                    || string.IsNullOrEmpty(warehouse.ZipCode) || string.IsNullOrEmpty(warehouse.City))
                return new GenericResponse(false, WarehouseResponseMessage.LocationDataNotProvided);

            warehouseToUpdate.City = warehouse.City;
            warehouseToUpdate.ContactPersonFirstName = warehouse.ContactPersonFirstName;
            warehouseToUpdate.ContactPersonLastName = warehouse.ContactPersonLastName;
            warehouseToUpdate.ContactPhoneNumber = warehouse.ContactPhoneNumber;
            warehouseToUpdate.Fax = warehouse.Fax;
            warehouseToUpdate.Mail = warehouse.Mail;
            warehouseToUpdate.Name = warehouse.Name;
            warehouseToUpdate.StreetAddress = warehouse.StreetAddress;
            warehouseToUpdate.ZipCode = warehouse.ZipCode;

            try
            {
                await unitOfWork.WarehouseRepository.UpdateWarehouse(warehouseToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, WarehouseResponseMessage.WarehouseUpdated);
        }
    }
}
