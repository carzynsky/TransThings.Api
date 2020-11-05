using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class ForwardingOrderService : IForwardingOrderService
    {
        private readonly IUnitOfWork unitOfWork;

        public ForwardingOrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public async Task<List<ForwardingOrder>> GetAllForwardingOrders()
        {
            var forwardingOrders = await unitOfWork.ForwardingOrderRepository.GetAllForwardingOrdersAsync();
            return forwardingOrders;
        }

        public async Task<List<ForwardingOrder>> GetForwardingOrdersByForwarder(int forwarderId)
        {
            var forwardingOrders = await unitOfWork.ForwardingOrderRepository.GetForwardingOrdersByForwarderAsync(forwarderId);
            return forwardingOrders;
        }

        /// <summary>
        /// Maybe it should be done not be foreach but by one efficient database request
        /// </summary>
        /// <param name="transitId"></param>
        /// <returns></returns>
        public async Task<List<ForwardingOrder>> GetForwardingOrdersByTransit(int transitId)
        {
            var transitsForwardingOrders = await unitOfWork.TransitForwardingOrderRepository.GetTransitForwardingOrdersByTransitAsync(transitId);
            if(transitsForwardingOrders != null)
            {
                List<ForwardingOrder> forwardingOrders = new List<ForwardingOrder>();
                foreach(var t in transitsForwardingOrders)
                {
                    var forwardingOrder = await unitOfWork.ForwardingOrderRepository.GetForwardingOrderByIdAsync(t.ForwardingOrderId);
                    if (forwardingOrder != null)
                        forwardingOrders.Add(forwardingOrder);
                }
                return forwardingOrders;
            }

            return null;
        }

        public async Task<ForwardingOrder> GetForwardingOrderById(int id)
        {
            var forwardingOrder = await unitOfWork.ForwardingOrderRepository.GetForwardingOrderByIdAsync(id);
            return forwardingOrder;
        }

        public async Task<GenericResponse> AddForwardingOrder(ForwardingOrder forwardingOrder)
        {
            if (forwardingOrder == null)
                return new GenericResponse(false, "No forwarding order has been provided.");

            if (forwardingOrder.ForwardingOrderNumber != null)
                return new GenericResponse(false, "Forwarding order number can't be provided manually.");

            if (forwardingOrder.CreateDate != null)
                return new GenericResponse(false, "Create date can't be provided manually.");

            forwardingOrder.ForwardingOrderNumber = await GenerateForwardingOrderNumber();
            forwardingOrder.CreateDate = DateTime.Now;

            try
            {
                await unitOfWork.ForwardingOrderRepository.AddForwardingOrderAsync(forwardingOrder);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "New forwarding order has been created.");
        }

        public async Task<GenericResponse> RemoveForwardingOrder(int id)
        {
            var forwardingOrderToRemove = await unitOfWork.ForwardingOrderRepository.GetForwardingOrderByIdAsync(id);
            if (forwardingOrderToRemove == null)
                return new GenericResponse(false, $"Forwarding order with id={id} does not exist.");

            try
            {
                await unitOfWork.ForwardingOrderRepository.RemoveForwardingOrder(forwardingOrderToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Forwarding order has been removed.");
        }

        public async Task<GenericResponse> UpdateForwardingOrder(ForwardingOrder forwardingOrder, int id)
        {
            if (forwardingOrder == null)
                return new GenericResponse(false, "Forwarding order has not been provided.");

            var forwardingOrderToUpdate = await unitOfWork.ForwardingOrderRepository.GetForwardingOrderByIdAsync(id);
            if (forwardingOrderToUpdate == null)
                return new GenericResponse(false, "Forwarding order with given id does not exist.");

            if (forwardingOrder.ForwardingOrderNumber != forwardingOrderToUpdate.ForwardingOrderNumber)
                return new GenericResponse(false, "Forwarding order number can't be modified.");

            if (forwardingOrder.CreateDate != forwardingOrderToUpdate.CreateDate)
                return new GenericResponse(false, "Create date can't be modified.");

            forwardingOrderToUpdate.AdditionalDescription = forwardingOrder.AdditionalDescription;
            forwardingOrderToUpdate.ForwarderId = forwardingOrder.ForwarderId;

            try
            {
                await unitOfWork.ForwardingOrderRepository.UpdateForwardingOrder(forwardingOrderToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Forwarding order has been updated.");
        }

        private async Task<string> GenerateForwardingOrderNumber()
        {
            string prefix = "ZS";
            string todayDate = DateTime.Now.ToString("ddMMyyyy");
            string timeNow = DateTime.Now.ToString("HHmmss");

            int lastForwardingOrderId;
            var lastForwardingOrder = await unitOfWork.ForwardingOrderRepository.GetLastForwardingOrderAsync();
            lastForwardingOrderId = lastForwardingOrder is null ? 1 : lastForwardingOrder.Id + 1;

            return string.Concat(prefix, "-", todayDate, "-", timeNow, "-", lastForwardingOrderId);
        }
    }
}
