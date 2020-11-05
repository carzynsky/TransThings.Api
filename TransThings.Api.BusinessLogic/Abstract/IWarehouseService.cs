using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IWarehouseService
    {
        Task<List<Warehouse>> GetAllWarehouses();
        Task<Warehouse> GetWarehouseById(int id);
        Task<GenericResponse> AddWarehouse(Warehouse warehouse);
        Task<GenericResponse> UpdateWarehouse(Warehouse warehouse, int id);
        Task<GenericResponse> RemoveWarehouse(int id);
    }
}
