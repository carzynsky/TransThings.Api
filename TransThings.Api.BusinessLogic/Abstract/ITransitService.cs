using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface ITransitService
    {
        Task<List<Transit>> GetAllTransits();
        Task<List<Transit>> GetTransitsByForwardingOrder(int forwardingOrderId);
        Task<Transit> GetTransitById(int id);
        Task<GenericResponse> AddTransit(Transit trasit);
        Task<GenericResponse> UpdateTransit(Transit transit, int id);
        Task<GenericResponse> RemoveTransit(int id);
    }
}
