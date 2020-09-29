using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface ITransitForwardingOrderService
    {
        Task<List<TransitForwardingOrder>> GetTransitsForwardingOrders();
        Task<TransitForwardingOrder> GetTransitForwardingOrder(int id);
        Task<GenericResponse> AddTransitForwardingOrder(TransitForwardingOrder transitForwardingOrder);
        Task<GenericResponse> UpdateTransitForwardingOrder(TransitForwardingOrder transitForwardingOrder, int id);
        Task<GenericResponse> RemoveTransitForwardingOrder(int id);
    }
}
