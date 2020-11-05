using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TransThings.Api.BusinessLogic.Abstract;
using TransThings.Api.BusinessLogic.Helpers;
using TransThings.Api.DataAccess.Models;
using TransThings.Api.DataAccess.RepositoryPattern;

namespace TransThings.Api.BusinessLogic.Services
{
    public class TransitForwardingOrderService : ITransitForwardingOrderService
    {
        private readonly IUnitOfWork unitOfWork;
        public TransitForwardingOrderService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        public async Task<List<TransitForwardingOrder>> GetTransitsForwardingOrders()
        {
            var transitsForwardingOrders = await unitOfWork.TransitForwardingOrderRepository.GetAllTransitForwardingOrdersAsync();
            return transitsForwardingOrders;
        }
        public async Task<TransitForwardingOrder> GetTransitForwardingOrder(int id)
        {
            var transitForwardingOrder = await unitOfWork.TransitForwardingOrderRepository.GetTransitForwardingOrderByIdAsync(id);
            return transitForwardingOrder;
        }

        public async Task<GenericResponse> AddTransitForwardingOrder(TransitForwardingOrder transitForwardingOrder)
        {
            if (transitForwardingOrder == null)
                return new GenericResponse(false, "Transit forwarding order relation data has not been provided.");

            try
            {
                await unitOfWork.TransitForwardingOrderRepository.AddTransitForwardingOrderAsync(transitForwardingOrder);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Transit - forwarding order relation has been added.");
        }

        public async Task<GenericResponse> RemoveTransitForwardingOrder(int id)
        {
            var transitForwardingOrderToRemove = await unitOfWork.TransitForwardingOrderRepository.GetTransitForwardingOrderByIdAsync(id);
            if (transitForwardingOrderToRemove == null)
                return new GenericResponse(false, $"Transit - forwarding order relation with id={id} does not exist.");

            try
            {
                await unitOfWork.TransitForwardingOrderRepository.RemoveTransitForwardingOrder(transitForwardingOrderToRemove);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Transit - forwarding order relation has been removed.");
        }

        public async Task<GenericResponse> UpdateTransitForwardingOrder(TransitForwardingOrder transitForwardingOrder, int id)
        {
            if (transitForwardingOrder == null)
                return new GenericResponse(false, "Transit - forwarding order relation has not been provided.");

            var transitForwardingOrderToUpdate = await unitOfWork.TransitForwardingOrderRepository.GetTransitForwardingOrderByIdAsync(id);
            if (transitForwardingOrderToUpdate == null)
                return new GenericResponse(false, $"Transit - forwarding order relation with id={id} does not exist.");
            /*
            if (transitForwardingOrder.TransitId == transitForwardingOrderToUpdate.TransitId && transitForwardingOrder.ForwardingOrderId == transitForwardingOrderToUpdate.ForwardingOrderId)
                return new GenericResponse(false, "No new changes has been provided.");
            */

            var transitForwardingOrderAlreadyInDatabase = await unitOfWork.TransitForwardingOrderRepository.GetTransitForwardingOrderByTransitAndForwardingOrderAsync(transitForwardingOrder.TransitId, transitForwardingOrder.ForwardingOrderId);
            if (transitForwardingOrderAlreadyInDatabase != null)
                return new GenericResponse(false, "Transit - forwarding order relation already exists in database.");

            transitForwardingOrderToUpdate.ForwardingOrderId = transitForwardingOrder.ForwardingOrderId;
            transitForwardingOrderToUpdate.TransitId = transitForwardingOrder.TransitId;

            try
            {
                await unitOfWork.TransitForwardingOrderRepository.AddTransitForwardingOrderAsync(transitForwardingOrderToUpdate);
            }
            catch (DbUpdateConcurrencyException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            catch (DbUpdateException ex)
            {
                return new GenericResponse(false, ex.InnerException.Message);
            }
            return new GenericResponse(true, "Transit - forwarding order relation has been updated.");
        }
    }
}
