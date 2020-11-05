using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Dto;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface ILoadService
    {
        Task<List<Load>> GetAllLoads();
        Task<List<Load>> GetLoadsByOrder(int orderId);
        Task<Load> GetLoadById(int id);
        Task<GenericResponse> AddLoad(LoadDto loads);
        Task<GenericResponse> UpdateLoad(Load load, int id);
        Task<GenericResponse> RemoveLoad(int id);
    }
}
