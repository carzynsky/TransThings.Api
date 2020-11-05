using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;

namespace TransThings.Api.BusinessLogic.Abstract
{
    public interface IOrderService
    {
        Task<List<Order>> GetAllOrders();
        Task<Order> GetOrderById(int id);
        Task<List<Order>> GetOrdersByClientId(int clientId);
        Task<List<Order>> GetOrdersByStatus(int orderStatusId);
        Task<List<Order>> GetOrdersByOrderer(int ordererId);
        Task<List<Order>> GetOrdersByConsultant(int consultantId);
        Task<List<Order>> GetOrdersByForwardingOrder(int forwardingOrderId);
        Task<OrderResponse> AddOrder(Order order);
        Task<GenericResponse> UpdateOrder(Order order, int id);
        Task<GenericResponse> RemoveOrder(int id);
    }
}
