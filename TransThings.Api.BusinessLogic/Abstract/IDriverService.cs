using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IDriverService
    {
        Task<List<Driver>> GetAllDrivers();
        Task<Driver> GetDriverById(int id);
        Task<List<Driver>> GetDriversByTransporter(int transporterId);
        Task<GenericResponse> AddDriver(Driver driver);
        Task<GenericResponse> UpdateDriver(Driver driver, int id);
        Task<GenericResponse> RemoveDriver(int id);
    }
}
