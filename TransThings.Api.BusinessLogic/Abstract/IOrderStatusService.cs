using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IOrderStatusService
    {
        Task<List<OrderStatus>> GetAllOrderStatuses();
        Task<OrderStatus> GetOrderStatusById(int id);
        Task<GenericResponse> AddOrderStatus(OrderStatus orderStatus);
        Task<GenericResponse> UpdateOrderStatus(OrderStatus orderStatus, int id);
        Task<GenericResponse> RemoveOrderStatus(int id);
    }
}
