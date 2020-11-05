using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;
using Microsoft.EntityFrameworkCore;

namespace TransThings.Api.BusinessLogic.Services
{
    public class OrderStatusService : IOrderStatusService
    {
        private readonly IUnitOfWork unitOfWork;

        public OrderStatusService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<OrderStatus>> GetAllOrderStatuses()
        {
            var orderStatuses = await unitOfWork.OrderStatusRepository.GetAllOrderStatusesAsync();
            return orderStatuses;
        }

        public async Task<OrderStatus> GetOrderStatusById(int id)
        {
            var orderStatus = await unitOfWork.OrderStatusRepository.GetOrderStatusByIdAsync(id);
            return orderStatus;
        }

        public async Task<GenericResponse> AddOrderStatus(OrderStatus orderStatus)
        {
            if (orderStatus == null)
                return new GenericResponse(false, "No order status has been provided.");

            if (string.IsNullOrEmpty(orderStatus.StatusName))
                return new GenericResponse(false, "Order status name has not been provided.");

            var orderStatusAlreadyInDb = await unitOfWork.OrderStatusRepository.GetOrderStatusByNameAsync(orderStatus.StatusName);
            if (orderStatusAlreadyInDb != null)
                return new GenericResponse(false, $"Order status {orderStatusAlreadyInDb.StatusName} already exists.");

            try
            {
                await unitOfWork.OrderStatusRepository.AddOrderStatusAsync(orderStatus);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "New order status has been created.");
        }

        public async Task<GenericResponse> RemoveOrderStatus(int id)
        {
            var orderStatusToRemove = await unitOfWork.OrderStatusRepository.GetOrderStatusByIdAsync(id);
            if (orderStatusToRemove == null)
                return new GenericResponse(false, $"Order status with id={id} does not exist.");

            try
            {
                await unitOfWork.OrderStatusRepository.RemoveOrderStatus(orderStatusToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Order status has been removed.");
        }

        public async Task<GenericResponse> UpdateOrderStatus(OrderStatus orderStatus, int id)
        {
            if (orderStatus == null)
                return new GenericResponse(false, "No order status has been provided.");

            var orderStatusToUpdate = await unitOfWork.OrderStatusRepository.GetOrderStatusByIdAsync(id);
            if (orderStatusToUpdate == null)
                return new GenericResponse(false, $"Order status with id={id} does not exist.");

            var orderStatusAlreadyInDb = await unitOfWork.OrderStatusRepository.GetOrderStatusByNameAsync(orderStatus.StatusName);
            if (orderStatusAlreadyInDb != null)
                return new GenericResponse(false, $"Order status {orderStatusAlreadyInDb.StatusName} already exists.");

            orderStatusToUpdate.StatusName = orderStatus.StatusName;

            try
            {
                await unitOfWork.OrderStatusRepository.UpdateOrderStatus(orderStatusToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Order status has been updated.");
        }
    }
}
