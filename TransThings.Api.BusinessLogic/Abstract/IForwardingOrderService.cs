using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IForwardingOrderService
    {
        Task<List<ForwardingOrder>> GetAllForwardingOrders();
        Task<List<ForwardingOrder>> GetForwardingOrdersByForwarder(int forwarderId);
        Task<List<ForwardingOrder>> GetForwardingOrdersByTransit(int transitId);
        Task<ForwardingOrder> GetForwardingOrderById(int id);
        Task<GenericResponse> AddForwardingOrder(ForwardingOrder forwardingOrder);
        Task<GenericResponse> UpdateForwardingOrder(ForwardingOrder forwardingOrder, int id);
        Task<GenericResponse> RemoveForwardingOrder(int id);
    }
}
