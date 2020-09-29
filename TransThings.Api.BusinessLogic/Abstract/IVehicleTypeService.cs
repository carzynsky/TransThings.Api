using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IVehicleTypeService
    {
        Task<List<VehicleType>> GetAllVehicleTypes();
        Task<VehicleType> GetVehicleTypeById(int id);
        Task<GenericResponse> AddVehicleType(VehicleType vehicleType);
        Task<GenericResponse> UpdateVehicleType(VehicleType vehicleType, int id);
        Task<GenericResponse> RemoveVehicleType(int id);
    }
}
