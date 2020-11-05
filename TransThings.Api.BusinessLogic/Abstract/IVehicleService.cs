using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IVehicleService
    {
        Task<List<Vehicle>> GetAllVehicles();
        Task<List<Vehicle>> GetVehiclesByTransporter(int transporterId);
        Task<Vehicle> GetVehicleById(int id);
        Task<GenericResponse> AddVehicle(Vehicle vehicle);
        Task<GenericResponse> UpdateVehicle(Vehicle vehicle, int id);
        Task<GenericResponse> RemoveVehicle(int id);
    }
}
