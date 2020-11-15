using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Dto;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface ITransitService
    {
        Task<List<Transit>> GetAllTransits();
        Task<List<Transit>> GetTransitsByForwardingOrder(int forwardingOrderId);
        Task<Transit> GetTransitById(int id);
        Task<GenericResponse> AddTransit(Transit trasit);
        Task<GenericResponse> UpdateTransits(TransitDto transits, int forwardingOrderId);
        Task<GenericResponse> RemoveTransit(int id);
    }
}
